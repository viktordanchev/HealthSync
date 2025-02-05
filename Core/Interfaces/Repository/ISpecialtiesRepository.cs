using Core.Models.ResponseDtos.Specialties;

namespace Core.Interfaces.Repository
{
    public interface ISpecialtiesRepository
    {
        Task<bool> IsSpecialtyExistAsync(int specialtyId);
        Task<IEnumerable<SpecialtyResponse>> GetSpecialtiesAsync();
    }
}
