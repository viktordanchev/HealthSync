using Core.DTOs.ResponseDtos.Hospitals;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;

namespace Core.Services
{
    public class HospitalsService : IHospitalsService
    {
        private readonly IHospitalsRepository _hospitalsRepo;

        public HospitalsService(IHospitalsRepository hospitalsRepo)
        {
            _hospitalsRepo = hospitalsRepo;
        }

        public async Task<IEnumerable<HospitalResponse>> GetHospitalsAsync()
        {
            var hospitals = await _hospitalsRepo.GetHospitalsAsync();

            return hospitals;
        }
    }
}
