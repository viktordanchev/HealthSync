using Core.Models.Doctor;
using Core.Models.ResponseDtos.Doctor;

namespace Core.Services.Contracts
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorResponse>> GetDoctorsAsync(int index, string sorting, string filter, string search);
        Task<DoctorDetailsResponse> GetDoctorAsync(int doctorId);
        Task<IEnumerable<ReviewResponse>> GetDoctorReviewsAsync(int index, int doctorId);
        Task<bool> IsDoctorExistAsync(int doctorId);
        Task AddReviewAsync(int doctorId, int rating, string comment, string reviewer);
        Task<IEnumerable<string>> GetSpecialtiesAsync();
        Task<bool> IsDayOffAsync(int doctorId, DateTime date);
        Task<IEnumerable<string>> GetAvailableMeetingsAsync(int doctorId, DateTime date);
        Task<IEnumerable<DayInMonthModel>> GetDaysInMonthAsync(int doctorId, int month, int year);
        Task AddMeetingAsync(int doctorId, DateTime date, string patientId);
        Task<bool> IsDateValidAsync(int doctorId, DateTime date);
    }
}
