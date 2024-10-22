using Core.ResponseDtos.Doctor;

namespace Core.Services.Contracts
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorProfileDto>> GetDoctors(int index, string sorting, string filter, string search);
        Task<IEnumerable<ReviewDto>> GetDoctorReviews(int index, string doctorId);
        Task<bool> IsDoctorExist(string doctorId);
        Task AddReview(string doctorId, int rating, string reviewer);
        Task<IEnumerable<string>> GetSpecialties();
    }
}
