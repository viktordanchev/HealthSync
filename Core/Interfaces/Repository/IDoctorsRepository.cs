using Core.DTOs.RequestDtos.Doctors;
using Core.DTOs.ResponseDtos.Doctors;

namespace Core.Interfaces.Repository
{
    public interface IDoctorsRepository
    {
        Task<IEnumerable<DoctorResponse>> GetDoctorsAsync(GetDoctorsRequest requestData, string userId);
        Task<DoctorDetailsResponse> GetDoctorDetailsAsync(int doctorId);
        Task<bool> IsDoctorExistAsync(int doctorId);
        Task<int> AddDoctorAsync(BecomeDoctorRequest requestData, string userId, string imgUrl);
        Task<bool> IsUserDoctorAsync(string userId);
        Task<DoctorPersonalInfoResponse> GetDoctorPersonalInfoAsync(string userId);
        Task GenerateEmptyDoctorWeekSchedule(int doctorId);
    }
}
