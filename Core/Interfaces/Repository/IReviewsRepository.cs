using Core.DTOs.RequestDtos.Reviews;
using Core.DTOs.ResponseDtos.Reviews;

namespace Core.Interfaces.Repository
{
    public interface IReviewsRepository
    {
        Task AddDoctorReviewAsync(AddReviewRequest requestData, string reviewer);
        Task<IEnumerable<ReviewResponse>> GetDoctorReviewsAsync(GetReviewsRequest requestData);
    }
}
