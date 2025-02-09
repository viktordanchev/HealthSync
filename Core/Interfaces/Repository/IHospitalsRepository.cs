using Core.DTOs.ResponseDtos.Hospitals;

namespace Core.Interfaces.Repository
{
    public interface IHospitalsRepository
    {
        Task<IEnumerable<HospitalResponse>> GetHospitalsAsync();
        Task<bool> IsHospitalExistAsync(int hospitalId);
    }
}
