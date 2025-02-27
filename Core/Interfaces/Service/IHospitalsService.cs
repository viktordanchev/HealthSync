using Core.DTOs.ResponseDtos.Hospitals;

namespace Core.Interfaces.Service
{
    public interface IHospitalsService
    {
        Task<IEnumerable<HospitalResponse>> GetHospitalsAsync();
    }
}
