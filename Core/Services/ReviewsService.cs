using Core.DTOs.RequestDtos.Reviews;
using Core.DTOs.ResponseDtos.Reviews;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;

namespace Core.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly IReviewsRepository _reviewsRepo;

        public ReviewsService(IReviewsRepository reviewsRepo)
        {
            _reviewsRepo = reviewsRepo;
        }

        public async Task AddDoctorReviewAsync(AddReviewRequest requestData, string reviewer)
        {
            await _reviewsRepo.AddDoctorReviewAsync(requestData, reviewer);
        }

        public async Task<IEnumerable<ReviewResponse>> GetDoctorReviewsAsync(GetReviewsRequest requestData)
        {
            var reviews = await _reviewsRepo.GetDoctorReviewsAsync(requestData);

            return reviews;
        }
    }
}
