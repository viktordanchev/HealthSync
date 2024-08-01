using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class HealthSyncDbContext : IdentityDbContext<IdentityUser>
    {
        public HealthSyncDbContext(DbContextOptions<HealthSyncDbContext> options) :
            base(options)
        { }
    }
}
