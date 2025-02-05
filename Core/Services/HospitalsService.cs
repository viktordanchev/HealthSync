using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Models.ResponseDtos.Hospitals;

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

        public async Task<bool> IsHospitalExistAsync(int hospitalId)
        {
            var isHospitalExist = await _hospitalsRepo.IsHospitalExistAsync(hospitalId);

            return isHospitalExist;
        }
    }
}
