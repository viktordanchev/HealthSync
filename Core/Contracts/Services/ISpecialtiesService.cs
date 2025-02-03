using Core.Models.ResponseDtos.Specialties;

namespace Core.Contracts.Services
{
    public interface ISpecialtiesService
    {
        Task<bool> IsSpecialtyExistAsync(int specialtyId);
        Task<IEnumerable<SpecialtyResponse>> GetSpecialtiesAsync();
    }
}
