using Infrastructure.Entities;
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
                Id = "d50a6c34-f6d3-4ff8-b1ad-299dcb776789",
                Type = "Orthodontist"
            };

            var specialty2 = new Specialty()
            {
                Id = "c3a9d3e1-5b4b-4ae1-b96c-8ef6bdb17c0d",
                Type = "Endocrinologist"
            };

            var specialty3 = new Specialty()
            {
                Id = "f24f8a2e-2f91-4be1-85a9-f1e6b4f82b74",
                Type = "Cardiologist"
            };

            var specialty4 = new Specialty()
            {
                Id = "01b2c6e4-cf32-4b44-84ae-4e4e2c17d8f9",
                Type = "Neurologist"
            };

            return [specialty1, specialty2, specialty3, specialty4];
        }
    }
}
