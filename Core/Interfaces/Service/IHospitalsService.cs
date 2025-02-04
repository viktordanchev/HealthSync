using Core.Models.ResponseDtos.Hospitals;

namespace Core.Interfaces.Service
{
    public interface IHospitalsService
    {
        Task<IEnumerable<HospitalResponse>> GetHospitalsAsync();
        Task<bool> IsHospitalExistAsync(int hospitalId);
    }
}
