using Core.DTOs.ResponseDtos.Hospitals;
using Core.Interfaces.Repository;

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

        public async Task<bool> IsHospitalExistAsync(int hospitalId)
        {
            var hospital = await _context.Hospitals.FirstOrDefaultAsync(h => h.Id == hospitalId);

            return hospital != null;
        }
    }
}
