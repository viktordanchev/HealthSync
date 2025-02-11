using Infrastructure.Database.Configs;
using Infrastructure.Database.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class HealthSyncDbContext : IdentityDbContext<ApplicationUser>
    {
        public HealthSyncDbContext(DbContextOptions<HealthSyncDbContext> options) :
            base(options)
        { }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<DoctorMeeting> DoctorsMeetings { get; set; }
        public DbSet<DoctorReview> DoctorsReviews { get; set; }
        public DbSet<DoctorSpecialty> DoctorSpecialties { get; set; }
        public DbSet<DoctorWeekDay> DoctorsWeekDays { get; set; }
        public DbSet<DoctorDayOff> DoctorsDaysOff { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationUserConfig());
            builder.ApplyConfiguration(new DoctorSpecialtyConfig());
            builder.ApplyConfiguration(new HospitalConfig());
            builder.ApplyConfiguration(new DoctorConfig());
            builder.ApplyConfiguration(new DoctorReviewConfig());
            builder.ApplyConfiguration(new DoctorWeekDayConfig());
            builder.ApplyConfiguration(new DoctorDayOffConfig());
            builder.ApplyConfiguration(new IdentityRoleConfig());
            builder.ApplyConfiguration(new IdentityUserRoleConfig());
            builder.ApplyConfiguration(new DoctorMeetingConfig());

            base.OnModelCreating(builder);
        }
    }
}
