using Core.DTOs.ResponseDtos.Specialties;
using Core.Interfaces.Repository;
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

        public async Task<IEnumerable<SpecialtyResponse>> GetSpecialtiesAsync()
        {
            var specialties = await _context.DoctorSpecialties
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
