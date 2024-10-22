using Core.ResponseDtos.Doctor;
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

        public async Task<IEnumerable<DoctorProfileDto>> GetDoctors(int index, string sorting, string filter, string search)
        {
            var doctors = await _context.Doctors
                .AsNoTracking()
                .Where(d => d.Identity.FirstName.Contains(search) 
                    || d.Identity.LastName.Contains(search) 
                    || d.Hospital.Name.Contains(search))
                .Skip(index * 10)
                .Take(10)
                .Select(d => new DoctorProfileDto()
                {
                    Id = d.Id,
                    Name = $"{d.Identity.FirstName} {d.Identity.LastName}",
                    ImgUrl = d.ImgUrl,
                    Specialty = d.Specialty.Type,
                    Hospital = d.Hospital.Name,
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

        public async Task<IEnumerable<ReviewDto>> GetDoctorReviews(int index, string doctorId)
        {
            var reviews = await _context.Reviews
                .AsNoTracking()
                .Where(r => r.DoctorId ==  doctorId)
                .OrderByDescending(r => r.Date)
                .Skip(index * 3)
                .Take(3)
                .Select(r => new ReviewDto()
                {
                    Rating = r.Rating,
                    Date = r.Date,
                    Reviewer = r.Reviewer
                })
                .ToListAsync();

            return reviews;
        }

        public async Task<bool> IsDoctorExist(string doctorId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);

            return doctor != null;
        }

        public async Task AddReview(string doctorId, int rating, string reviewer)
        {
            var reveiew = new Review()
            {
                Id = Guid.NewGuid().ToString(),
                DoctorId = doctorId,
                Rating = rating,
                Date = DateTime.Now,
                Reviewer = reviewer
            };

            await _context.Reviews.AddAsync(reveiew);
            await _context.SaveChangesAsync();
        }
    }
}
