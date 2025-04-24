using Core.DTOs.RequestDtos.Doctors;
using Core.DTOs.ResponseDtos.Doctors;

namespace Core.Interfaces.Repository
{
    public interface IDoctorsRepository
    {
        Task<IEnumerable<DoctorResponse>> GetDoctorsAsync(GetDoctorsRequest requestData, string userEmail);
        Task<DoctorDetailsResponse> GetDoctorDetailsAsync(int doctorId);
        Task<int> AddDoctorAsync(ProfileInfoRequest requestData, string userId, string imgUrl);
        Task<DoctorPersonalInfoResponse> GetDoctorPersonalInfoAsync(string userId);
        Task GenerateEmptyDoctorWeekSchedule(int doctorId);
        Task<int> GetDoctorIdAsync(string userId);
        Task<IEnumerable<DoctorResponse>> GetTopDoctorsAsync();
        Task UpdateProfileInfo(ProfileInfoRequest requestData, string userId);
    }
}
