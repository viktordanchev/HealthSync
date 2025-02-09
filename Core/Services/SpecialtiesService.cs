using Core.DTOs.ResponseDtos.Specialties;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;

namespace Core.Services
{
    public class SpecialtiesService : ISpecialtiesService
    {
        private readonly ISpecialtiesRepository _specialtiesRepo;

        public SpecialtiesService(ISpecialtiesRepository specialtiesRepo)
        {
            _specialtiesRepo = specialtiesRepo;
        }

        public async Task<bool> IsSpecialtyExistAsync(int specialtyId)
        {
            var isSpecialtyExist = await _specialtiesRepo.IsSpecialtyExistAsync(specialtyId);

            return isSpecialtyExist;
        }

        public async Task<IEnumerable<SpecialtyResponse>> GetSpecialtiesAsync()
        {
            var specialties = await _specialtiesRepo.GetSpecialtiesAsync();

            return specialties;
        }
    }
}
