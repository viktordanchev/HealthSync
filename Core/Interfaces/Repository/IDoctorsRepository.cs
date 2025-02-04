using Core.Models.ResponseDtos.Doctors;
using RestAPI.Dtos.RequestDtos.Doctors;

namespace Core.Interfaces.Repository
{
    public interface IDoctorsRepository
    {
        Task<IEnumerable<DoctorResponse>> GetDoctorsAsync(GetDoctorsRequest requestData, string userIdentityId);
        Task<DoctorDetailsResponse> GetDoctorDetailsAsync(int doctorId);
        Task<bool> IsDoctorExistAsync(int doctorId);
        Task AddDoctorAsync(BecomeDoctorRequest requestData, string userIdentityId, string imgUrl);
        Task<bool> IsUserDoctorAsync(string userId);
        Task<DoctorPersonalInfoResponse> GetDoctorPersonalInfoAsync(string userId);
        Task GenerateEmptyDoctorWeekSchedule(int doctorId);
    }
}
