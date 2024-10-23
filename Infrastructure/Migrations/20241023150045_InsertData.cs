using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InsertData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Meetings");

            migrationBuilder.AddColumn<string>(
                name: "Patient",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "4d650e24-6b66-41e3-8391-efab8c31a1dd", 0, "02efdcf8-50ab-46f7-a873-129c35a665ed", "m.marinova@mail.com", true, "Maria", "Marinova", false, null, "M.MARINOVA@MAIL.COM", "M.MARINOVA@MAIL.COM", "AQAAAAIAAYagAAAAEFMIpeqk08LVOKPnEPty4i2ky49QGTN+ierwjSwFqljWRhVRbZ0nnlBdtlbssgwTlw==", null, false, "58c42a9d-731f-43e7-bef3-644e48509cd7", false, "m.marinova@mail.com" },
                    { "78850da7-a0ff-42f3-a862-d162457910a0", 0, "4fadfe12-4079-4565-9d97-deb8108ed090", "v.yankova@mail.com", true, "Vanya", "Yankova", false, null, "V.YANKOVA@MAIL.COM", "V.YANKOVA@MAIL.COM", "AQAAAAIAAYagAAAAEAnkVzM52wuZVZSsL97C5FxJtCaYSc8KlVreISablLsioqROPNdnhAwhPEkk+Pb/lg==", null, false, "62c478c9-cbfe-4056-a835-dc69476783ec", false, "v.yankova@mail.com" },
                    { "88cd5a7b-01d8-49b4-8688-35cd23751532", 0, "f7667197-9666-48d4-b554-70a72f212e44", "a.kirilov@mail.com", true, "Aleks", "Kirilov", false, null, "A.KIRILOV@MAIL.COM", "A.KIRILOV@MAIL.COM", "AQAAAAIAAYagAAAAEMbbr650RgqCHgzpLgJc1kqH0xe2YRs4cexF+7XHI/yQ04qHMckKnkbWwZNzbJZn2A==", null, false, "6bf9ebd8-73e9-40a4-a6ea-a215ce4151af", false, "a.kirilov@mail.com" },
                    { "95189f02-fb1a-4700-95e3-6146b8aa8b15", 0, "57f30552-5e97-4bef-a75a-15d7407a6afd", "k.conev@mail.com", true, "Kiril", "Conev", false, null, "K.CONEV@MAIL.COM", "K.CONEV@MAIL.COM", "AQAAAAIAAYagAAAAECIJUr87ERn0kCTnGXOLhgS/wNVUTPDn5SUvefwKNQY+jwg2zqsCTT/v67mx2b/KhQ==", null, false, "e35bca95-e6d6-49ea-89b5-d76a74be8aeb", false, "k.conev@mail.com" },
                    { "a3717562-385e-41ce-9eff-0f1b994e5548", 0, "a459b61c-cf0c-4c32-acb3-0c6181cab47e", "i.ivanov@mail.com", true, "Ivan", "Ivanov", false, null, "I.IVANOV@MAIL.COM", "I.IVANOV@MAIL.COM", "AQAAAAIAAYagAAAAELIgs921MICrbsvHlhXu8JUywlZD+3HJJGr043npw2512sXEDR+w0/viRvACSABxUA==", null, false, "b0ca130b-c15f-4b86-a816-876dc1bd5825", false, "i.ivanov@mail.com" },
                    { "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35", 0, "ddd3b61d-4267-4bdb-9607-6ffc12d7fb11", "m.kirilova@mail.com", true, "Monika", "Kirilova", false, null, "M.KIRILOVA@MAIL.COM", "M.KIRILOVA@MAIL.COM", "AQAAAAIAAYagAAAAEMRAeXfipOIIDMEaChZv6j2koS2g1GPKHu/Pxhf2c0ptI0jTOqSHKUQqSMRXZ5jiVw==", null, false, "29ef2d16-8b08-4045-b3aa-217d8fcc5787", false, "m.kirilova@mail.com" },
                    { "f37b43ca-86a2-4b11-972d-5e0569f4deb3", 0, "71769f68-8f2b-49e3-8b30-4325fb69fd2c", "i.ivanova@mail.com", true, "Ivana", "Ivanova", false, null, "I.IVANOVA@MAIL.COM", "I.IVANOVA@MAIL.COM", "AQAAAAIAAYagAAAAEKaF5SmwHlOF8EBl3d38CXpd38Pp4yicYDTZ6A+VpE4/HSA5eWp6CgkHYtOY5H6tAQ==", null, false, "705a53ae-2a80-4ca2-bd67-320f32d8c602", false, "i.ivanova@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { "505d9322-7cca-4bca-ae59-683ff3089872", "456 Sunrise Avenue, Clearwater, FL 33759, USA", "Sunnybrook General Hospital" },
                    { "710649bb-deb0-4271-a97f-6e5cde3d2fe6", "321 Maple Street, Boulder, CO 80301, USA", "Pine Hills Medical Center" }
                });

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { "01b2c6e4-cf32-4b44-84ae-4e4e2c17d8f9", "Neurologist" },
                    { "c3a9d3e1-5b4b-4ae1-b96c-8ef6bdb17c0d", "Endocrinologist" },
                    { "d50a6c34-f6d3-4ff8-b1ad-299dcb776789", "Orthodontist" },
                    { "f24f8a2e-2f91-4be1-85a9-f1e6b4f82b74", "Cardiologist" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "HospitalId", "IdenitityId", "ImgUrl", "SpecialtyId" },
                values: new object[,]
                {
                    { "43eb5263-a106-4d5b-909f-92294b21f360", "505d9322-7cca-4bca-ae59-683ff3089872", "4d650e24-6b66-41e3-8391-efab8c31a1dd", "https://storage.cloud.google.com/healthsync/maria-marinova.jpg", "c3a9d3e1-5b4b-4ae1-b96c-8ef6bdb17c0d" },
                    { "4411897e-f897-404e-95bd-85e683b34ff5", "505d9322-7cca-4bca-ae59-683ff3089872", "88cd5a7b-01d8-49b4-8688-35cd23751532", "https://storage.cloud.google.com/healthsync/aleks-kirilov.jpg", "c3a9d3e1-5b4b-4ae1-b96c-8ef6bdb17c0d" },
                    { "44a35b22-2cdb-44bd-8286-d7ec7eaa2248", "505d9322-7cca-4bca-ae59-683ff3089872", "a3717562-385e-41ce-9eff-0f1b994e5548", "https://storage.cloud.google.com/healthsync/ivan-ivanov.jpg", "d50a6c34-f6d3-4ff8-b1ad-299dcb776789" },
                    { "6ba6d91c-3c17-4a90-a73b-87085a17861a", "710649bb-deb0-4271-a97f-6e5cde3d2fe6", "78850da7-a0ff-42f3-a862-d162457910a0", null, "d50a6c34-f6d3-4ff8-b1ad-299dcb776789" },
                    { "702abf92-a237-460a-94b5-5763127fb627", "710649bb-deb0-4271-a97f-6e5cde3d2fe6", "f37b43ca-86a2-4b11-972d-5e0569f4deb3", "https://storage.cloud.google.com/healthsync/ivana-ivanova.jpg", "01b2c6e4-cf32-4b44-84ae-4e4e2c17d8f9" },
                    { "77bb4f69-7cb7-4a8d-acb2-5464b74cfff1", "710649bb-deb0-4271-a97f-6e5cde3d2fe6", "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35", "https://storage.cloud.google.com/healthsync/monika-kirilova.jpg", "f24f8a2e-2f91-4be1-85a9-f1e6b4f82b74" },
                    { "97e6c00a-664f-4861-8976-4058aed9cdb0", "505d9322-7cca-4bca-ae59-683ff3089872", "95189f02-fb1a-4700-95e3-6146b8aa8b15", "https://storage.cloud.google.com/healthsync/kiril-conev.jpg", "f24f8a2e-2f91-4be1-85a9-f1e6b4f82b74" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Date", "DoctorId", "Rating", "Reviewer" },
                values: new object[,]
                {
                    { "1f4ac07c-673a-4936-b0e4-196dd7488192", new DateTime(2023, 1, 24, 19, 52, 0, 0, DateTimeKind.Unspecified), "44a35b22-2cdb-44bd-8286-d7ec7eaa2248", 4, "Kristin Angelova" },
                    { "2ba604e3-80d8-40cb-8861-5d07ba7107e7", new DateTime(2024, 8, 4, 7, 0, 0, 0, DateTimeKind.Unspecified), "44a35b22-2cdb-44bd-8286-d7ec7eaa2248", 6, "Kristian Ivanov" },
                    { "2c840f56-b9b4-414e-976c-c694dc4e5e80", new DateTime(2024, 7, 20, 18, 52, 0, 0, DateTimeKind.Unspecified), "44a35b22-2cdb-44bd-8286-d7ec7eaa2248", 6, "Kosta Adamovich" },
                    { "4d3e3800-b989-4406-85cc-f0ecd1080b88", new DateTime(2024, 9, 19, 10, 32, 0, 0, DateTimeKind.Unspecified), "44a35b22-2cdb-44bd-8286-d7ec7eaa2248", 2, "Maria Kostova" },
                    { "b2089e63-a041-4638-b308-c3ab8b6551ea", new DateTime(2022, 12, 12, 10, 43, 0, 0, DateTimeKind.Unspecified), "43eb5263-a106-4d5b-909f-92294b21f360", 5, "Kristian Ivanov" },
                    { "ba9d730a-fcf5-4662-adfd-23bfa49e10f1", new DateTime(2024, 10, 14, 9, 0, 0, 0, DateTimeKind.Unspecified), "44a35b22-2cdb-44bd-8286-d7ec7eaa2248", 5, "Aleks Petrov" },
                    { "bd5b349e-9eba-4778-91cc-9ea18d3a41a3", new DateTime(2023, 1, 25, 13, 12, 0, 0, DateTimeKind.Unspecified), "44a35b22-2cdb-44bd-8286-d7ec7eaa2248", 3, "Angel Bogdanski" },
                    { "c6f58c38-5d0c-4006-8697-ac72ee11fe29", new DateTime(2024, 4, 2, 8, 2, 0, 0, DateTimeKind.Unspecified), "43eb5263-a106-4d5b-909f-92294b21f360", 5, "Yordan Angelov" },
                    { "d6a1dcfd-418b-4304-8744-d341beab0cf6", new DateTime(2024, 6, 15, 22, 12, 0, 0, DateTimeKind.Unspecified), "44a35b22-2cdb-44bd-8286-d7ec7eaa2248", 2, "Viktor Terziev" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: "4411897e-f897-404e-95bd-85e683b34ff5");

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: "6ba6d91c-3c17-4a90-a73b-87085a17861a");

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: "702abf92-a237-460a-94b5-5763127fb627");

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: "77bb4f69-7cb7-4a8d-acb2-5464b74cfff1");

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: "97e6c00a-664f-4861-8976-4058aed9cdb0");

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: "1f4ac07c-673a-4936-b0e4-196dd7488192");

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: "2ba604e3-80d8-40cb-8861-5d07ba7107e7");

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: "2c840f56-b9b4-414e-976c-c694dc4e5e80");

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: "4d3e3800-b989-4406-85cc-f0ecd1080b88");

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: "b2089e63-a041-4638-b308-c3ab8b6551ea");

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: "ba9d730a-fcf5-4662-adfd-23bfa49e10f1");

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: "bd5b349e-9eba-4778-91cc-9ea18d3a41a3");

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: "c6f58c38-5d0c-4006-8697-ac72ee11fe29");

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: "d6a1dcfd-418b-4304-8744-d341beab0cf6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "78850da7-a0ff-42f3-a862-d162457910a0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "88cd5a7b-01d8-49b4-8688-35cd23751532");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "95189f02-fb1a-4700-95e3-6146b8aa8b15");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f37b43ca-86a2-4b11-972d-5e0569f4deb3");

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: "43eb5263-a106-4d5b-909f-92294b21f360");

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: "44a35b22-2cdb-44bd-8286-d7ec7eaa2248");

            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: "710649bb-deb0-4271-a97f-6e5cde3d2fe6");

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: "01b2c6e4-cf32-4b44-84ae-4e4e2c17d8f9");

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: "f24f8a2e-2f91-4be1-85a9-f1e6b4f82b74");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d650e24-6b66-41e3-8391-efab8c31a1dd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a3717562-385e-41ce-9eff-0f1b994e5548");

            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: "505d9322-7cca-4bca-ae59-683ff3089872");

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: "c3a9d3e1-5b4b-4ae1-b96c-8ef6bdb17c0d");

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: "d50a6c34-f6d3-4ff8-b1ad-299dcb776789");

            migrationBuilder.DropColumn(
                name: "Patient",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
