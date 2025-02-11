using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs
{
    public class DayOffConfig : IEntityTypeConfiguration<DayOff>
    {
        public void Configure(EntityTypeBuilder<DayOff> builder)
        {
            builder.HasData(SeedDaysOff());
        }

        private DayOff[] SeedDaysOff()
        {
            var dayOff1 = new DayOff()
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
