using Core.DTOs.ResponseDtos.Specialties;

namespace Core.Interfaces.Service
{
    public interface ISpecialtiesService
    {
        Task<IEnumerable<SpecialtyResponse>> GetSpecialtiesAsync();
    }
}
