using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs
{
    public class DoctorMeetingConfig : IEntityTypeConfiguration<DoctorMeeting>
    {
        public void Configure(EntityTypeBuilder<DoctorMeeting> builder)
        {
            builder
                .HasOne(m => m.Doctor)
                .WithMany(d => d.Meetings)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(m => m.Patient)
                .WithMany(p => p.Meetings)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(dm => dm.DateAndTime)
                .HasColumnType("timestamp without time zone");
        }
    }
}
