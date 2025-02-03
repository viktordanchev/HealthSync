using Core.Models.ResponseDtos.Hospitals;

namespace Core.Contracts.Services
{
    public interface IHospitalsService
    {
        Task<IEnumerable<HospitalResponse>> GetHospitalsAsync();
        Task<bool> IsHospitalExistAsync(int hospitalId);
    }
}
