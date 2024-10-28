using Infrastructure.Configurations;
using Infrastructure.Entities;
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
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<WorkDay> WorkDays { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationUserConfig());
            builder.ApplyConfiguration(new SpecialtyConfig());
            builder.ApplyConfiguration(new HospitalConfig());
            builder.ApplyConfiguration(new DoctorConfig());
            builder.ApplyConfiguration(new ReviewConfig());
            builder.ApplyConfiguration(new MeetingConfig());
        
            base.OnModelCreating(builder);
        }
    }
}
