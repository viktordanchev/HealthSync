using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class SpecialtyConfig : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.HasData(SeedSpecialties());
        }

        private Specialty[] SeedSpecialties()
        {
            var specialty1 = new Specialty()
            {
                Id = 1,
                Type = "Orthodontist"
            };

            var specialty2 = new Specialty()
            {
                Id = 2,
                Type = "Endocrinologist"
            };

            var specialty3 = new Specialty()
            {
                Id = 3,
                Type = "Cardiologist"
            };

            var specialty4 = new Specialty()
            {
                Id = 4,
                Type = "Neurologist"
            };

            return [specialty1, specialty2, specialty3, specialty4];
        }
    }
}
