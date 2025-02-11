using Core.DTOs.RequestDtos.Reviews;
using Core.DTOs.ResponseDtos.Reviews;
using Core.Interfaces.Repository;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class ReviewsRepository : IReviewsRepository
    {
        private readonly HealthSyncDbContext _context;

        public ReviewsRepository(HealthSyncDbContext context)
        {
            _context = context;
        }

        public async Task AddDoctorReviewAsync(AddReviewRequest requestData, string reviewer)
        {
            var reveiew = new DoctorReview()
            {
                DoctorId = requestData.DoctorId,
                Rating = requestData.Rating,
                DateAndTime = DateTime.Now,
                Comment = requestData.Comment,
                Reviewer = reviewer
            };

            await _context.DoctorsReviews.AddAsync(reveiew);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReviewResponse>> GetDoctorReviewsAsync(GetReviewsRequest requestData)
        {
            var reviews = await _context.DoctorsReviews
                .AsNoTracking()
                .Where(r => r.DoctorId == requestData.DoctorId)
                .OrderByDescending(r => r.DateAndTime)
                .Skip(requestData.Index * 3)
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
