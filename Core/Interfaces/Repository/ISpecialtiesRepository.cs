using Core.DTOs.ResponseDtos.Specialties;

namespace Core.Interfaces.Repository
{
    public interface ISpecialtiesRepository
    {
        Task<bool> IsSpecialtyExistAsync(int specialtyId);
        Task<IEnumerable<SpecialtyResponse>> GetSpecialtiesAsync();
    }
}
