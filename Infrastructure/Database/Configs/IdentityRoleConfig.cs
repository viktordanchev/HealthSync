using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs
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
                Id = "87ac5db3-1586-4580-8037-ed108b66a9b1",
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
