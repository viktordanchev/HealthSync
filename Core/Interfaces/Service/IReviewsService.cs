using Core.DTOs.RequestDtos.Reviews;
using Core.DTOs.ResponseDtos.Reviews;

namespace Core.Interfaces.Service
{
    public interface IReviewsService
    {
        Task AddDoctorReviewAsync(AddReviewRequest requestData, string reviewer);
        Task<IEnumerable<ReviewResponse>> GetDoctorReviewsAsync(GetReviewsRequest requestData);
    }
}
