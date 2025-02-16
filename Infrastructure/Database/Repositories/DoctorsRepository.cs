﻿using Core.DTOs.RequestDtos.Doctors;
using Core.DTOs.ResponseDtos.Doctors;
using Core.Interfaces.Repository;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class DoctorsRepository : IDoctorsRepository
    {
        private readonly HealthSyncDbContext _context;

        public DoctorsRepository(HealthSyncDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DoctorResponse>> GetDoctorsAsync(GetDoctorsRequest requestData, string userEmail)
        {
            var doctors = await _context.Doctors
                .AsNoTracking()
                .Where(d => ((string.IsNullOrEmpty(requestData.Search) ||
                    string.Concat(d.Identity.FirstName, " ", d.Identity.LastName).Contains(requestData.Search)) &&
                    (string.IsNullOrEmpty(requestData.Filter) || d.Specialty.Type == requestData.Filter)) &&
                    d.Identity.Email != userEmail)
                .Skip(requestData.Index * 10)
                .Take(10)
                .Select(d => new DoctorResponse()
                {
                    Id = d.Id,
                    Name = $"{d.Identity.FirstName} {d.Identity.LastName}",
                    ImgUrl = d.ImgUrl,
                    Specialty = d.Specialty.Type,
                    Rating = d.Reviews.Any() ? Math.Round(d.Reviews.Average(r => r.Rating), 1) : 0,
                    TotalReviews = d.Reviews.Where(r => r.DoctorId == d.Id).Count()
                })
                .ToListAsync();

            return doctors;
        }

        public async Task<DoctorDetailsResponse> GetDoctorDetailsAsync(int doctorId)
        {
            var doctorDetails = await _context.Doctors
                .AsNoTracking()
                .Where(d => d.Id == doctorId)
                .Select(d => new DoctorDetailsResponse()
                {
                    Id = d.Id,
                    Name = $"{d.Identity.FirstName} {d.Identity.LastName}",
                    ImgUrl = d.ImgUrl,
                    Specialty = d.Specialty.Type,
                    Rating = d.Reviews.Any() ? Math.Round(d.Reviews.Average(r => r.Rating), 1) : 0,
                    TotalReviews = d.Reviews.Where(r => r.DoctorId == d.Id).Count(),
                    HospitalName = d.Hospital.Name,
                    HospitalAddress = d.Hospital.Address,
                    Information = d.Information,
                    ContactEmail = d.ContactEmail,
                    ContactPhoneNumber = d.ContactPhoneNumber
                })
                .FirstAsync();

            return doctorDetails;
        }

        public async Task<bool> IsDoctorExistAsync(int doctorId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);

            return doctor != null;
        }

        public async Task<int> AddDoctorAsync(BecomeDoctorRequest requestData, string userId, string imgUrl)
        {
            var doctor = new Doctor()
            {
                IdentityId = userId,
                HospitalId = requestData.HospitalId,
                SpecialtyId = requestData.SpecialtyId,
                ContactEmail = requestData.ContactEmail,
                ContactPhoneNumber = requestData.ContactPhoneNumber,
                ImgUrl = imgUrl
            };

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            return doctor.Id;
        }

        public async Task<bool> IsUserDoctorAsync(string userId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.IdentityId == userId);

            return doctor != null;
        }

        public async Task<DoctorPersonalInfoResponse> GetDoctorPersonalInfoAsync(string userId)
        {
            var doctorInfo = await _context.Doctors
                .Where(d => d.IdentityId == userId)
                .Select(d => new DoctorPersonalInfoResponse()
                { 
                    Name = $"{d.Identity.FirstName} {d.Identity.LastName}",
                    ImgUrl = d.ImgUrl,
                    HospitalId = d.HospitalId,
                    Hospital = d.Hospital.Name,
                    SpecialtyId = d.SpecialtyId,
                    Specialty = d.Specialty.Type,
                    PersonalInformation = d.Information,
                    ContactEmail = d.ContactEmail,
                    ContactPhoneNumber = d.ContactPhoneNumber,
                    WeeklySchedule = d.WorkWeek
                        .Select(wd => new WeekDayResponse()
                        {
                            Id = wd.Id,
                            WeekDay = wd.WeekDay,
                            IsWorkDay = wd.IsWorkDay,
                            WorkDayStart = wd.WorkDayStart,
                            WorkDayEnd = wd.WorkDayEnd,
                            MeetingTimeMinutes = wd.MeetingTimeMinutes
                        })
                        .ToList()
                })
                .FirstAsync();

            return doctorInfo;
        }

        public async Task GenerateEmptyDoctorWeekSchedule(int doctorId)
        {
            var week = new List<DoctorWeekDay>();

            for (int day = 0; day < 7; day++)
            {
                var workDay = new DoctorWeekDay()
                {
                    DoctorId = doctorId,
                    WeekDay = (DayOfWeek)((day + 1) % 7),
                    IsWorkDay = false
                };
            }

            await _context.DoctorsWeekDays.AddRangeAsync(week);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetDoctorIdAsync(string userId)
        {
            var doctor = await _context.Doctors
                .FirstAsync(d => d.IdentityId == userId);

            return doctor.Id;
        }
    }
}
