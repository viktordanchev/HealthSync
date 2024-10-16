﻿using Core.DTOs.Doctor;
using Core.Services.Contracts;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly HealthSyncDbContext _context;

        public DoctorService(HealthSyncDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DoctorProfileDto>> GetDoctors(string sorting, string filter, string search)
        {
            var doctors = await _context.Doctors
                .AsNoTracking()
                .Where(d => d.Identity.FirstName.Contains(search) 
                    || d.Identity.LastName.Contains(search) 
                    || d.Hospital.Name.Contains(search))
                .Select(d => new DoctorProfileDto()
                {
                    Id = d.Id,
                    Name = $"{d.Identity.FirstName} {d.Identity.LastName}",
                    ImgUrl = d.ImgUrl,
                    Specialty = d.Specialty.Type,
                    Hospital = d.Hospital.Name,
                    Raiting = d.Reviews.Any() ? Math.Round(d.Reviews.Average(r => r.Rating), 1) : 0,
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
                    doctors = doctors.OrderBy(d => d.Raiting).ToList();
                    break;
                case "RatingDesc":
                    doctors = doctors.OrderByDescending(d => d.Raiting).ToList();
                    break;
            }

            return doctors;
        }

        public async Task<IEnumerable<ReviewDto>> GetDoctorReviews(string doctorId)
        {
            var reviews = await _context.Reviews
                .AsNoTracking()
                .Where(r => r.DoctorId ==  doctorId)
                .Select(r => new ReviewDto()
                {
                    Rating = r.Rating,
                    Date = r.Date,
                    Reviewer = r.Reviewer
                })
                .OrderByDescending(r => r.Date)
                .ToListAsync();

            return reviews;
        }
    }
}
