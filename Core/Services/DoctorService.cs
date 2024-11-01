﻿using Core.Models.Doctor;
using Core.Models.ResponseDtos.Doctor;
using Core.Services.Contracts;
using Infrastructure;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Core.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly HealthSyncDbContext _context;

        public DoctorService(HealthSyncDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DoctorProfileResponse>> GetDoctors(int index, string sorting, string filter, string search)
        {
            var doctors = await _context.Doctors
                .AsNoTracking()
                .Where(d => (string.IsNullOrEmpty(search) ||
                    string.Concat(d.Identity.FirstName, " ", d.Identity.LastName).Contains(search)) &&
                    (string.IsNullOrEmpty(filter) || d.Specialty.Type == filter))
                .Skip(index * 10)
                .Take(10)
                .Select(d => new DoctorProfileResponse()
                {
                    Id = d.Id,
                    Name = $"{d.Identity.FirstName} {d.Identity.LastName}",
                    ImgUrl = d.ImgUrl,
                    Specialty = d.Specialty.Type,
                    Hospital = d.Hospital.Name,
                    HospitalAddress = d.Hospital.Address,
                    Rating = d.Reviews.Any() ? Math.Round(d.Reviews.Average(r => r.Rating), 1) : 0,
                    TotalReviews = d.Reviews.Where(r => r.DoctorId == d.Id).Count()
                })
                .ToListAsync();

            switch (sorting)
            {
                case "NameAsc":
                    doctors = doctors.OrderBy(d => d.Name).ToList();
                    break;
                case "NameDesc":
                    doctors = doctors.OrderByDescending(d => d.Name).ToList();
                    break;
                case "RatingAsc":
                    doctors = doctors.OrderBy(d => d.Rating).ToList();
                    break;
                case "RatingDesc":
                    doctors = doctors.OrderByDescending(d => d.Rating).ToList();
                    break;
            }

            return doctors;
        }

        public async Task<IEnumerable<ReviewResponse>> GetDoctorReviews(int index, int doctorId)
        {
            var reviews = await _context.Reviews
                .AsNoTracking()
                .Where(r => r.DoctorId == doctorId)
                .OrderByDescending(r => r.Date)
                .Skip(index * 3)
                .Take(3)
                .Select(r => new ReviewResponse()
                {
                    Rating = r.Rating,
                    Date = r.Date,
                    Reviewer = r.Reviewer
                })
                .ToListAsync();

            return reviews;
        }

        public async Task<bool> IsDoctorExist(int doctorId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);

            return doctor != null;
        }

        public async Task AddReview(int doctorId, int rating, string reviewer)
        {
            var reveiew = new Review()
            {
                DoctorId = doctorId,
                Rating = rating,
                Date = DateTime.Now,
                Reviewer = reviewer
            };

            await _context.Reviews.AddAsync(reveiew);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetSpecialties()
        {
            var specialties = await _context.Specialties
                .AsNoTracking()
                .OrderBy(s => s.Type)
                .Select(s => s.Type)
                .ToListAsync();

            return specialties;
        }

        public async Task<bool> IsDayOff(int doctorId, DateTime date)
        {
            var doctor = await _context.Doctors
                .AsNoTracking()
                .Where(d => d.Id == doctorId)
                .FirstAsync();

            return doctor.DaysOff.Any(d => d.Date == date);
        }

        public async Task<IEnumerable<string>> GetAvailableMeetings(int doctorId, DateTime date)
        {
            var workTime = await _context.Doctors
                .AsNoTracking()
                .Where(d => d.Id == doctorId)
                .Select(d => new DoctorModel()
                {
                    MeetingTimeMinutes = d.MeetingTimeMinutes,
                    WorkDayStart = d.WorkWeek.First(wd => wd.Day == date.DayOfWeek).Start,
                    WorkDayEnd = d.WorkWeek.First(wd => wd.Day == date.DayOfWeek).End,
                    Meetings = d.Meetings
                        .Where(m => m.Date.Date == date)
                        .Select(m => m.Date.TimeOfDay)
                        .ToList()
                })
                .FirstAsync();

            var availableMeetings = new List<string>();

            while (workTime.WorkDayStart <= workTime.WorkDayEnd)
            {
                if (!workTime.Meetings.Contains(workTime.WorkDayStart))
                {
                    availableMeetings.Add($"{workTime.WorkDayStart.Hours:D2} : {workTime.WorkDayStart.Minutes:D2}");
                }

                workTime.WorkDayStart = workTime.WorkDayStart.Add(TimeSpan.FromMinutes(workTime.MeetingTimeMinutes));
            }

            return availableMeetings;
        }

        public async Task<IEnumerable<string>> GetDaysOffByMonth(int doctorId, int month, int year)
        {
            var daysOffd = await _context.Doctors
                .AsNoTracking()
                .Where(d => d.Id == doctorId)
                .Select(d => new Model()
                {
                    DaysOff = d.DaysOff
                        .Select(doff => doff.Date)
                        .ToList(),
                    WeeklyDaysOff = d.WorkWeek
                        .Where(wk => !wk.IsWorkDay)
                        .Select(wk => wk.Day)
                        .ToList()
                })
                .FirstAsync();

            int days = DateTime.DaysInMonth(year, month);

            var daysOff = new List<string>();

            for (int day = 1; day <= days; day++)
            {
                var date = new DateTime(year, month, day);

                if(daysOffd.DaysOff.Contains(date) || daysOffd.WeeklyDaysOff.Contains(date.DayOfWeek))
                {
                    daysOff.Add(date.ToString("yyyy-MM-dd"));
                }
            }

            return daysOff;
        }
    }
}
