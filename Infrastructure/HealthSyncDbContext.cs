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
    }
}
