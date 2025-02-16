using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs
{
    public class DoctorDayOffConfig : IEntityTypeConfiguration<DoctorDayOff>
    {
        public void Configure(EntityTypeBuilder<DoctorDayOff> builder)
        {
            builder.HasData(SeedDaysOff());
        }

        private DoctorDayOff[] SeedDaysOff()
        {
            var dayOff1 = new DoctorDayOff()
            {
                Id = 1,
                DoctorId = 1,
                Month = 12,
                Day = 25,
            };

            return [dayOff1];
        }
    }
}
