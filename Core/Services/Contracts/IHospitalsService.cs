using Core.Models.ResponseDtos.Hospitals;

namespace Core.Services.Contracts
{
    public interface IHospitalsService
    {
        Task<IEnumerable<HospitalResponse>> GetHospitalsAsync();
        Task<bool> IsHospitalExistAsync(int hospitalId);
    }
}
