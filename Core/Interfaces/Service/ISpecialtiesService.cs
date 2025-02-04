using Core.Models.ResponseDtos.Specialties;

namespace Core.Interfaces.Service
{
    public interface ISpecialtiesService
    {
        Task<bool> IsSpecialtyExistAsync(int specialtyId);
        Task<IEnumerable<SpecialtyResponse>> GetSpecialtiesAsync();
    }
}
