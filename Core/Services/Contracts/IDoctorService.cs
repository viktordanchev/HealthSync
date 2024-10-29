using Core.ResponseDtos.Doctor;

namespace Core.Services.Contracts
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorProfileResponse>> GetDoctors(int index, string sorting, string filter, string search);
        Task<IEnumerable<ReviewResponse>> GetDoctorReviews(int index, int doctorId);
        Task<bool> IsDoctorExist(int doctorId);
        Task AddReview(int doctorId, int rating, string reviewer);
        Task<IEnumerable<string>> GetSpecialties();
        Task<IEnumerable<TimeSpan>> GetMeetings(int doctorId, DayOfWeek dayOfWeek);
    }
}
