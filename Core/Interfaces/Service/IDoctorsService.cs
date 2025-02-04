using Core.Models.ResponseDtos.Doctors;
using RestAPI.Dtos.RequestDtos.Doctors;

namespace Core.Interfaces.Service
{
    public interface IDoctorsService
    {
        Task<IEnumerable<DoctorResponse>> GetDoctorsAsync(GetDoctorsRequest requestData, string userIdentityId);
        Task<DoctorDetailsResponse> GetDoctorDetailsAsync(int doctorId);
        Task<bool> IsDoctorExistAsync(int doctorId);
        Task AddDoctorAsync(string userId, int hospitalId, int specialtyId, string contactEmail, string contactPhoneNumber, string? imgUrl);
        Task<bool> IsUserDoctorAsync(string userId);
        Task<DoctorPersonalInfoResponse> GetDoctorPersonalInfoAsync(string userId);
    }
}
