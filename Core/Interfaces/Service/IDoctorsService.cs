using Core.DTOs.RequestDtos.Doctors;
using Core.DTOs.ResponseDtos.Doctors;

namespace Core.Interfaces.Service
{
    public interface IDoctorsService
    {
        Task<IEnumerable<DoctorResponse>> GetDoctorsAsync(GetDoctorsRequest requestData, string userEmail);
        Task<DoctorDetailsResponse> GetDoctorDetailsAsync(int doctorId);
        Task<bool> IsDoctorExistAsync(int doctorId);
        Task AddDoctorAsync(BecomeDoctorRequest requestData, string userId);
        Task<bool> IsUserDoctorAsync(string userId);
        Task<DoctorPersonalInfoResponse> GetDoctorPersonalInfoAsync(string userId);
    }
}
