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
                .HasOne(m => m.Doctor)
                .WithMany(d => d.Meetings)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(m => m.Patient)
                .WithMany(p => p.Meetings)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
