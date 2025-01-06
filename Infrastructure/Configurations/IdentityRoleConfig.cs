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

            var role2 = new IdentityRole()
            {
                Id = "bd4718fa-f25a-40ab-abce-8261cc3ea8e8",
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            return [role1, role2];
        }
    }
}
