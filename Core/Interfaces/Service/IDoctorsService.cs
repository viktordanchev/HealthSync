using Core.Models.ResponseDtos.Doctors;
using RestAPI.Dtos.RequestDtos.Doctors;

namespace Core.Interfaces.Service
{
    public interface IDoctorsService
    {
        Task<IEnumerable<DoctorResponse>> GetDoctorsAsync(GetDoctorsRequest requestData, string userId);
        Task<DoctorDetailsResponse> GetDoctorDetailsAsync(int doctorId);
        Task<bool> IsDoctorExistAsync(int doctorId);
        Task AddDoctorAsync(BecomeDoctorRequest requestData, string userId);
        Task<bool> IsUserDoctorAsync(string userId);
        Task<DoctorPersonalInfoResponse> GetDoctorPersonalInfoAsync(string userId);
    }
}
