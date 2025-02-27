using Core.DTOs.ResponseDtos.Hospitals;
using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class HospitalsRepository : IHospitalsRepository
    {
        private readonly HealthSyncDbContext _context;

        public HospitalsRepository(HealthSyncDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HospitalResponse>> GetHospitalsAsync()
        {
            var hospitals = await _context.Hospitals
                .AsNoTracking()
                .Select(h => new HospitalResponse()
                {
                    Id = h.Id,
                    Name = h.Name
                })
                .ToListAsync();

            return hospitals;
        }
    }
}
