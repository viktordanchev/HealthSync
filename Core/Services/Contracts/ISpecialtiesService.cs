using Core.Models.ResponseDtos.Specialties;

namespace Core.Services.Contracts
{
    public interface ISpecialtiesService
    {
        Task<bool> IsSpecialtyExistAsync(int specialtyId);
        Task<IEnumerable<SpecialtyResponse>> GetSpecialtiesAsync();
    }
}
