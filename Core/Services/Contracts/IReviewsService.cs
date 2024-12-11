using Core.Models.ResponseDtos.Reviews;

namespace Core.Services.Contracts
{
    public interface IReviewsService
    {
        Task AddDoctorReviewAsync(int doctorId, int rating, string comment, string reviewer);
        Task<IEnumerable<ReviewResponse>> GetDoctorReviewsAsync(int index, int doctorId);
    }
}
