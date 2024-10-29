﻿using Core.ResponseDtos.Doctor;
using Core.Services.Contracts;
using Infrastructure;
using Infrastructure.Entities;
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

        public async Task<IEnumerable<TimeSpan>> GetMeetings(int doctorId, DayOfWeek dayOfWeek)
        {
            //var meetings = await _context.WorkDays
            //    .AsNoTracking()
            //    .Where(m => m.WorkSchedule.DoctorId == doctorId
            //        && m.Day == dayOfWeek)
            //    .ToListAsync();

            Console.WriteLine();
            return null;
        }
    }
}
