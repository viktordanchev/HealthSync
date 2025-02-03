using Infrastructure.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(SeedUsers());
        }

        private ApplicationUser[] SeedUsers()
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            var user1 = new ApplicationUser()
            {
                Id = "a3717562-385e-41ce-9eff-0f1b994e5548",
                UserName = "i.ivanov@mail.com",
                NormalizedUserName = "I.IVANOV@MAIL.COM",
                Email = "i.ivanov@mail.com",
                NormalizedEmail = "I.IVANOV@MAIL.COM",
                FirstName = "Ivan",
                LastName = "Ivanov",
                EmailConfirmed = true
            };
            user1.PasswordHash = hasher.HashPassword(user1, "123456");

            var user2 = new ApplicationUser()
            {
                Id = "4d650e24-6b66-41e3-8391-efab8c31a1dd",
                UserName = "m.marinova@mail.com",
                NormalizedUserName = "M.MARINOVA@MAIL.COM",
                Email = "m.marinova@mail.com",
                NormalizedEmail = "M.MARINOVA@MAIL.COM",
                FirstName = "Maria",
                LastName = "Marinova",
                EmailConfirmed = true
            };
            user2.PasswordHash = hasher.HashPassword(user2, "123456");

            var user3 = new ApplicationUser()
            {
                Id = "88cd5a7b-01d8-49b4-8688-35cd23751532",
                UserName = "a.kirilov@mail.com",
                NormalizedUserName = "A.KIRILOV@MAIL.COM",
                Email = "a.kirilov@mail.com",
                NormalizedEmail = "A.KIRILOV@MAIL.COM",
                FirstName = "Aleks",
                LastName = "Kirilov",
                EmailConfirmed = true
            };
            user3.PasswordHash = hasher.HashPassword(user3, "123456");

            var user4 = new ApplicationUser()
            {
                Id = "95189f02-fb1a-4700-95e3-6146b8aa8b15",
                UserName = "k.conev@mail.com",
                NormalizedUserName = "K.CONEV@MAIL.COM",
                Email = "k.conev@mail.com",
                NormalizedEmail = "K.CONEV@MAIL.COM",
                FirstName = "Kiril",
                LastName = "Conev",
                EmailConfirmed = true
            };
            user4.PasswordHash = hasher.HashPassword(user4, "123456");

            var user5 = new ApplicationUser()
            {
                Id = "f37b43ca-86a2-4b11-972d-5e0569f4deb3",
                UserName = "i.ivanova@mail.com",
                NormalizedUserName = "I.IVANOVA@MAIL.COM",
                Email = "i.ivanova@mail.com",
                NormalizedEmail = "I.IVANOVA@MAIL.COM",
                FirstName = "Ivana",
                LastName = "Ivanova",
                EmailConfirmed = true
            };
            user5.PasswordHash = hasher.HashPassword(user5, "123456");

            var user6 = new ApplicationUser()
            {
                Id = "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35",
                UserName = "m.kirilova@mail.com",
                NormalizedUserName = "M.KIRILOVA@MAIL.COM",
                Email = "m.kirilova@mail.com",
                NormalizedEmail = "M.KIRILOVA@MAIL.COM",
                FirstName = "Monika",
                LastName = "Kirilova",
                EmailConfirmed = true
            };
            user6.PasswordHash = hasher.HashPassword(user6, "123456");

            var user7 = new ApplicationUser()
            {
                Id = "78850da7-a0ff-42f3-a862-d162457910a0",
                UserName = "v.yankova@mail.com",
                NormalizedUserName = "V.YANKOVA@MAIL.COM",
                Email = "v.yankova@mail.com",
                NormalizedEmail = "V.YANKOVA@MAIL.COM",
                FirstName = "Vanya",
                LastName = "Yankova",
                EmailConfirmed = true
            };
            user7.PasswordHash = hasher.HashPassword(user7, "123456");

            return [ user1, user2, user3, user4, user5, user6, user7 ];
        }
    }
}
