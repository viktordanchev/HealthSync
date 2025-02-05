using Core.Models.ResponseDtos.Reviews;
using RestAPI.Dtos.RequestDtos.Reviews;

namespace Core.Interfaces.Repository
{
    public interface IReviewsRepository
    {
        Task AddDoctorReviewAsync(AddReviewRequest requestData, string reviewer);
        Task<IEnumerable<ReviewResponse>> GetDoctorReviewsAsync(GetReviewsRequest requestData);
    }
}
