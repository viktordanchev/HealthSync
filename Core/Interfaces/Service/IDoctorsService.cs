using Core.DTOs.RequestDtos.Doctors;
using Core.DTOs.ResponseDtos.Doctors;
using Core.DTOs.ResponseDtos.Specialties;

namespace Core.Interfaces.Service
{
    public interface IDoctorsService
    {
        Task<IEnumerable<DoctorResponse>> GetDoctorsAsync(GetDoctorsRequest requestData, string userEmail);
        Task<DoctorDetailsResponse> GetDoctorDetailsAsync(int doctorId);
        Task AddDoctorAsync(ProfileInfoRequest requestData, string userId);
        Task<DoctorPersonalInfoResponse> GetDoctorPersonalInfoAsync(string userId);
        Task<IEnumerable<SpecialtyResponse>> GetSpecialtiesAsync();
        Task<IEnumerable<DoctorResponse>> GetTopDoctorsAsync();
        Task UpdateProfileInfoAsync(ProfileInfoRequest requestData, string userId);
    }
}
