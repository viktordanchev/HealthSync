using Core.Models.Doctor;
using Core.Services.Contracts;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly HealthSyncDbContext _context;

        public DoctorService(HealthSyncDbContext context)
        {
            _context = context; 
        }

        public async Task<IEnumerable<DoctorDto>> GetAll()
        {
            var doctors = await _context.Doctors
                .Select(d => new DoctorDto()
                {
                    Name = $"{d.Identity.FirstName} {d.Identity.LastName}",
                    Hospital = d.Hospital.Name,
                    Raiting = d.Reviews.Average(r => r.Rating),
                })
                .ToListAsync();

            return doctors;
        }
    }
}
