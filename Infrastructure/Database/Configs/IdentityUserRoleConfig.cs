using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs
{
    public class IdentityUserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(SeedUserRoles());
        }

        private IdentityUserRole<string>[] SeedUserRoles()
        {
            var userRole1 = new IdentityUserRole<string>()
            {
                RoleId = "87ac5db3-1586-4580-8037-ed108b66a9b1",
                UserId = "a3717562-385e-41ce-9eff-0f1b994e5548" //Ivan Ivanov
            };

            var userRole2 = new IdentityUserRole<string>()
            {
                RoleId = "87ac5db3-1586-4580-8037-ed108b66a9b1",
                UserId = "4d650e24-6b66-41e3-8391-efab8c31a1dd" //Maria Marinova
            };

            var userRole3 = new IdentityUserRole<string>()
            {
                RoleId = "87ac5db3-1586-4580-8037-ed108b66a9b1",
                UserId = "88cd5a7b-01d8-49b4-8688-35cd23751532" //Aleks Kirilov
            };

            var userRole4 = new IdentityUserRole<string>()
            {
                RoleId = "87ac5db3-1586-4580-8037-ed108b66a9b1",
                UserId = "95189f02-fb1a-4700-95e3-6146b8aa8b15" //Kiril Conev
            };

            var userRole5 = new IdentityUserRole<string>()
            {
                RoleId = "87ac5db3-1586-4580-8037-ed108b66a9b1",
                UserId = "f37b43ca-86a2-4b11-972d-5e0569f4deb3" //Ivana Ivanova
            };

            var userRole6 = new IdentityUserRole<string>()
            {
                RoleId = "87ac5db3-1586-4580-8037-ed108b66a9b1",
                UserId = "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35" //Monika Kirilova
            };

            var userRole7 = new IdentityUserRole<string>()
            {
                RoleId = "87ac5db3-1586-4580-8037-ed108b66a9b1",
                UserId = "78850da7-a0ff-42f3-a862-d162457910a0" //Vanya Yankova
            };

            return [userRole1, userRole2, userRole3, userRole4, userRole5, userRole6, userRole7];
        }
    }
}
