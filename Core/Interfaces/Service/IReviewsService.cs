using Core.Models.ResponseDtos.Reviews;

namespace Core.Interfaces.Service
{
    public interface IReviewsService
    {
        Task AddDoctorReviewAsync(int doctorId, int rating, string comment, string reviewer);
        Task<IEnumerable<ReviewResponse>> GetDoctorReviewsAsync(int index, int doctorId);
    }
}
