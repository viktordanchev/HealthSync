using Core.Models.ResponseDtos.Doctors;

namespace Core.Services.Contracts
{
    public interface IDoctorsService
    {
        Task<IEnumerable<DoctorResponse>> GetDoctorsAsync(int index, string sorting, string filter, string search, string doctorIdentityId);
        Task<DoctorDetailsResponse> GetDoctorDetailsAsync(int doctorId);
        Task<bool> IsDoctorExistAsync(int doctorId);
        Task<bool> IsSpecialtyExistAsync(int specialtyId);
        Task<IEnumerable<SpecialtyResponse>> GetSpecialtiesAsync();
        Task AddDoctorAsync(string userId, int hospitalId, int specialtyId, string contactEmail, string contactPhoneNumber, string? imgUrl);
        Task<bool> IsUserDoctorAsync(string userId);
        Task<DoctorInfoModel> GetDoctorPersonalInfoAsync(string userId);
    }
}
