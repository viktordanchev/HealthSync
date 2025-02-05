using Core.Interfaces.Repository;
using Core.Models.ResponseDtos.Specialties;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class SpecialtiesRepository : ISpecialtiesRepository
    {
        private readonly HealthSyncDbContext _context;

        public SpecialtiesRepository(HealthSyncDbContext context)
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
