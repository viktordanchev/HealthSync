using Core.Contracts.Services;
using Core.Models.ResponseDtos.Hospitals;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class HospitalsService : IHospitalsService
    {
        private readonly HealthSyncDbContext _context;

        public HospitalsService(HealthSyncDbContext context)
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
