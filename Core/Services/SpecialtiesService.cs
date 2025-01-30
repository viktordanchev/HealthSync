using Core.Models.ResponseDtos.Specialties;
using Core.Services.Contracts;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class SpecialtiesService : ISpecialtiesService
    {
        private readonly HealthSyncDbContext _context;

        public SpecialtiesService(HealthSyncDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsSpecialtyExistAsync(int specialtyId)
        {
            var specialty = await _context.Specialties.FirstOrDefaultAsync(s => s.Id == specialtyId);

            return specialty != null;
        }

        public async Task<IEnumerable<SpecialtyResponse>> GetSpecialtiesAsync()
        {
            var specialties = await _context.Specialties
                .AsNoTracking()
                .Select(s => new SpecialtyResponse()
                {
                    Id = s.Id,
                    Name = s.Type
                })
                .ToListAsync();

            return specialties;
        }
    }
}
