using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs
{
    public class HospitalConfig : IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            builder.HasData(SeedHospitals());
        }

        private Hospital[] SeedHospitals()
        {
            var hospital1 = new Hospital()
            {
                Id = 1,
                Name = "Sunnybrook General Hospital",
                Address = "456 Sunrise Avenue, Clearwater, FL 33759, USA"
            };

            var hospital2 = new Hospital()
            {
                Id = 2,
                Name = "Pine Hills Medical Center",
                Address = "321 Maple Street, Boulder, CO 80301, USA"
            };

            return [hospital1, hospital2];
        }
    }
}
