using Core.DTOs.Doctor;

namespace Core.Services.Contracts
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorProfileDto>> GetDoctors(string sorting, string filter, string search);
        Task<IEnumerable<ReviewDto>> GetDoctorReviews(string doctorId);
    }
}
