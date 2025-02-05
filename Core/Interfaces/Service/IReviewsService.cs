using Core.Models.ResponseDtos.Reviews;
using RestAPI.Dtos.RequestDtos.Reviews;

namespace Core.Interfaces.Service
{
    public interface IReviewsService
    {
        Task AddDoctorReviewAsync(AddReviewRequest requestData, string reviewer);
        Task<IEnumerable<ReviewResponse>> GetDoctorReviewsAsync(GetReviewsRequest requestData);
    }
}
