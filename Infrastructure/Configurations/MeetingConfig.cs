using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class MeetingConfig : IEntityTypeConfiguration<Meeting>
    {
        public void Configure(EntityTypeBuilder<Meeting> builder)
        {
            builder
                .HasOne(m => m.WorkSchedule)
                .WithMany(ws => ws.Meetings)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
