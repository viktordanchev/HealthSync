using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
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
                Date = new DateTime(2024, 12, 25, 0, 0, 0)
            };

            return [dayOff1];
        }
    }
}
