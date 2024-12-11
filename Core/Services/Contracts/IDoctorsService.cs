using Core.Models.ResponseDtos.Doctor;

namespace Core.Services.Contracts
{
    public interface IDoctorsService
    {
        Task<IEnumerable<DoctorResponse>> GetDoctorsAsync(int index, string sorting, string filter, string search, string doctorIdentityId);
        Task<DoctorDetailsResponse> GetDoctorDetailsAsync(int doctorId);
        Task<bool> IsDoctorExistAsync(int doctorId);
        Task<IEnumerable<SpecialtyResponse>> GetSpecialtiesAsync();
    }
}
