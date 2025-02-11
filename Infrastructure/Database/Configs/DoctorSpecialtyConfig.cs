using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs
{
    public class DoctorSpecialtyConfig : IEntityTypeConfiguration<DoctorSpecialty>
    {
        public void Configure(EntityTypeBuilder<DoctorSpecialty> builder)
        {
            builder.HasData(SeedSpecialties());
        }

        private DoctorSpecialty[] SeedSpecialties()
        {
            var specialty1 = new DoctorSpecialty()
            {
                Id = 1,
                Type = "Orthodontist"
            };

            var specialty2 = new DoctorSpecialty()
            {
                Id = 2,
                Type = "Endocrinologist"
            };

            var specialty3 = new DoctorSpecialty()
            {
                Id = 3,
                Type = "Cardiologist"
            };

            var specialty4 = new DoctorSpecialty()
            {
                Id = 4,
                Type = "Neurologist"
            };

            return [specialty1, specialty2, specialty3, specialty4];
        }
    }
}
