﻿using Core.Models.ResponseDtos.Doctors;
using Core.Services.Contracts;
using Infrastructure;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class DoctorsService : IDoctorsService
    {
        private readonly HealthSyncDbContext _context;

        public DoctorsService(HealthSyncDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DoctorResponse>> GetDoctorsAsync(int index, string sorting, string filter, string search, string doctorIdentityId)
        {
            var doctors = await _context.Doctors
                .AsNoTracking()
                .Where(d => ((string.IsNullOrEmpty(search) ||
                    string.Concat(d.Identity.FirstName, " ", d.Identity.LastName).Contains(search)) &&
                    (string.IsNullOrEmpty(filter) || d.Specialty.Type == filter)) &&
                    d.IdentityId != doctorIdentityId)
                .Skip(index * 10)
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

        public async Task<DoctorDetailsResponse> GetDoctorDetailsAsync(int doctorId)
        {
            var doctor = await _context.Doctors
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
                .FirstOrDefaultAsync();

            return doctor;
        }

        public async Task<bool> IsDoctorExistAsync(int doctorId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);

            return doctor != null;
        }

        public async Task<bool> IsSpecialtyExistAsync(int specialtyId)
        {
            var specialty = await _context.Specialties.FirstOrDefaultAsync(s => s.Id == specialtyId);

            return specialty != null;
        }

        public async Task<IEnumerable<SpecialtyResponse>> GetSpecialtiesAsync()
        {
            var specialties = await _context.Specialties
                .AsNoTracking()
                .Select(s => new SpecialtyResponse()
                {
                    Id = s.Id,
                    Name = s.Type
                })
                .ToListAsync();

            return specialties;
        }

        public async Task AddDoctorAsync(string userId, int hospitalId, int specialtyId, string contactEmail, string contactPhoneNumber, string? imgUrl)
        {
            var doctor = new Doctor()
            {
                IdentityId = userId,
                HospitalId = hospitalId,
                SpecialtyId = specialtyId,
                ContactEmail = contactEmail,
                ContactPhoneNumber = contactPhoneNumber,
                ImgUrl = imgUrl
            };

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUserDoctorAsync(string userId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.IdentityId == userId);

            return doctor != null;
        }
    }
}