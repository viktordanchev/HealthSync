using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
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
                Id = "505d9322-7cca-4bca-ae59-683ff3089872",
                Name = "Sunnybrook General Hospital",
                Address = "456 Sunrise Avenue, Clearwater, FL 33759, USA"
            };

            var hospital2 = new Hospital()
            {
                Id = "710649bb-deb0-4271-a97f-6e5cde3d2fe6",
                Name = "Pine Hills Medical Center",
                Address = "321 Maple Street, Boulder, CO 80301, USA"
            };


            return [hospital1, hospital2];
        }
    }
}
