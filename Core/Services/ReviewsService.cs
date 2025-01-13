using Core.Models.ResponseDtos.Reviews;
using Core.Services.Contracts;
using Infrastructure;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly HealthSyncDbContext _context;

        public ReviewsService(HealthSyncDbContext context)
        {
            _context = context;
        }

        public async Task AddDoctorReviewAsync(int doctorId, int rating, string comment, string reviewer)
        {
            var reveiew = new Review()
            {
                DoctorId = doctorId,
                Rating = rating,
                DateAndTime = DateTime.Now,
                Comment = comment,
                Reviewer = reviewer
            };

            await _context.Reviews.AddAsync(reveiew);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReviewResponse>> GetDoctorReviewsAsync(int index, int doctorId)
        {
            var reviews = await _context.Reviews
                .AsNoTracking()
                .Where(r => r.DoctorId == doctorId)
                .OrderByDescending(r => r.DateAndTime)
                .Skip(index * 3)
                .Take(3)
                .Select(r => new ReviewResponse()
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    DateAndTime = r.DateAndTime,
                    Comment = r.Comment,
                    Reviewer = r.Reviewer
                })
                .ToListAsync();

            return reviews;
        }
    }
}
