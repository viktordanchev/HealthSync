using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class IdentityRoleConfig : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(SeedRoles());
        }

        private IdentityRole[] SeedRoles()
        {
            var role1 = new IdentityRole()
            {
                Id = "a550b411-e5c7-407f-aaba-35161bab127e",
                Name = "Doctor",
                NormalizedName = "DOCTOR"
            };

            return [role1];
        }
    }
}
