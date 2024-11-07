using Core.Models.Doctor;
using Core.Models.ResponseDtos.Doctor;

namespace Core.Services.Contracts
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorProfileResponse>> GetDoctors(int index, string sorting, string filter, string search);
        Task<IEnumerable<ReviewResponse>> GetDoctorReviews(int index, int doctorId);
        Task<bool> IsDoctorExist(int doctorId);
        Task AddReview(int doctorId, int rating, string comment, string reviewer);
        Task<IEnumerable<string>> GetSpecialties();
        Task<bool> IsDayOff(int doctorId, DateTime date);
        Task<IEnumerable<string>> GetAvailableMeetings(int doctorId, DateTime date);
        Task<IEnumerable<DayOfWeekModel>> GetDaysInMonth(int doctorId, int month, int year);
    }
}
