using Core.DTOs.ResponseDtos.Specialties;

namespace Core.Interfaces.Repository
{
    public interface ISpecialtiesRepository
    {
        Task<IEnumerable<SpecialtyResponse>> GetSpecialtiesAsync();
    }
}
