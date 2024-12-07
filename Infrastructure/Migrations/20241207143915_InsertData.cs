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
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a550b411-e5c7-407f-aaba-35161bab127e", null, "Doctor", "DOCTOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "4d650e24-6b66-41e3-8391-efab8c31a1dd", 0, "f54d61b5-80bc-41f0-9f4d-5739ac634286", "m.marinova@mail.com", true, "Maria", "Marinova", false, null, "M.MARINOVA@MAIL.COM", "M.MARINOVA@MAIL.COM", "AQAAAAIAAYagAAAAEAT6Cy86PCjVvEDHat4EImD8a5R6Q0cGTcx97ebtPf7YMAZrseGxF5VTZ9IL9tdSLQ==", null, false, "460f99ed-fb01-4952-aba0-67397b792197", false, "m.marinova@mail.com" },
                    { "78850da7-a0ff-42f3-a862-d162457910a0", 0, "b8cd85b4-408e-44ac-9067-7edfb17b8cf3", "v.yankova@mail.com", true, "Vanya", "Yankova", false, null, "V.YANKOVA@MAIL.COM", "V.YANKOVA@MAIL.COM", "AQAAAAIAAYagAAAAEAgSq7KnzUfSgHwd0OVrZtJzGETXDm8OAEgCMpu1NYbeSIKC+/oJR8+L9j/lMiCEIA==", null, false, "12a18c5b-4ea6-4aad-9228-e22fa81c0836", false, "v.yankova@mail.com" },
                    { "88cd5a7b-01d8-49b4-8688-35cd23751532", 0, "e7b75324-95c1-454a-af03-27ada1e69e00", "a.kirilov@mail.com", true, "Aleks", "Kirilov", false, null, "A.KIRILOV@MAIL.COM", "A.KIRILOV@MAIL.COM", "AQAAAAIAAYagAAAAEJDNjfPR9eAbgeyZMBhTFUV8QEDDVJ6utl3Hwm86u9tXN5hOvi9Wi7fKaRjwF0TVNg==", null, false, "61320d93-5871-494b-9713-e5ee81dcda44", false, "a.kirilov@mail.com" },
                    { "95189f02-fb1a-4700-95e3-6146b8aa8b15", 0, "efef7e1c-f76e-4a0e-ba44-d2d73875e76b", "k.conev@mail.com", true, "Kiril", "Conev", false, null, "K.CONEV@MAIL.COM", "K.CONEV@MAIL.COM", "AQAAAAIAAYagAAAAEKwdqT/A7Byk3tnUOz34wLLw/nBvfVNcA16TEPUUQs71DY+pETkRHVCvPUyUIb+Gvw==", null, false, "8ecb3ffb-370d-4299-90d3-71ec914111bc", false, "k.conev@mail.com" },
                    { "a3717562-385e-41ce-9eff-0f1b994e5548", 0, "4a7ad467-1691-46a2-bdc4-db7fb95200d7", "i.ivanov@mail.com", true, "Ivan", "Ivanov", false, null, "I.IVANOV@MAIL.COM", "I.IVANOV@MAIL.COM", "AQAAAAIAAYagAAAAEFLkE86bX+ahht/iMj3XDFMVP7PSwonu+0UDybv+e2g02Tr0610eSgqDQ+jjQsYwrA==", null, false, "30c91b63-5556-42e7-880d-c4b362addc21", false, "i.ivanov@mail.com" },
                    { "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35", 0, "e2804d13-de94-428a-9f3e-df00497aa28b", "m.kirilova@mail.com", true, "Monika", "Kirilova", false, null, "M.KIRILOVA@MAIL.COM", "M.KIRILOVA@MAIL.COM", "AQAAAAIAAYagAAAAEBMwpr5MXmiULdd7sGv65jwBUTPM1uaMy8FO0HNorl+QfiBVsJHq8gwEEAH4WqoPfQ==", null, false, "9f8a6563-c206-48f3-bcea-55679cf50659", false, "m.kirilova@mail.com" },
                    { "f37b43ca-86a2-4b11-972d-5e0569f4deb3", 0, "721d4329-2792-42ea-ac43-f9236f9be0df", "i.ivanova@mail.com", true, "Ivana", "Ivanova", false, null, "I.IVANOVA@MAIL.COM", "I.IVANOVA@MAIL.COM", "AQAAAAIAAYagAAAAEKbzTv2oy1Ot8jnh/OCZEWH0+quv0imTRW00CL0U5bMEQQdT22i9Hh4ga19sHkzKqg==", null, false, "e5228783-2a0c-43f5-9000-3c8d3f744a70", false, "i.ivanova@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { 1, "456 Sunrise Avenue, Clearwater, FL 33759, USA", "Sunnybrook General Hospital" },
                    { 2, "321 Maple Street, Boulder, CO 80301, USA", "Pine Hills Medical Center" }
                });

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Orthodontist" },
                    { 2, "Endocrinologist" },
                    { 3, "Cardiologist" },
                    { 4, "Neurologist" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "a550b411-e5c7-407f-aaba-35161bab127e", "4d650e24-6b66-41e3-8391-efab8c31a1dd" },
                    { "a550b411-e5c7-407f-aaba-35161bab127e", "78850da7-a0ff-42f3-a862-d162457910a0" },
                    { "a550b411-e5c7-407f-aaba-35161bab127e", "88cd5a7b-01d8-49b4-8688-35cd23751532" },
                    { "a550b411-e5c7-407f-aaba-35161bab127e", "95189f02-fb1a-4700-95e3-6146b8aa8b15" },
                    { "a550b411-e5c7-407f-aaba-35161bab127e", "a3717562-385e-41ce-9eff-0f1b994e5548" },
                    { "a550b411-e5c7-407f-aaba-35161bab127e", "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35" },
                    { "a550b411-e5c7-407f-aaba-35161bab127e", "f37b43ca-86a2-4b11-972d-5e0569f4deb3" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "HospitalId", "IdentityId", "ImgUrl", "Information", "MeetingTimeMinutes", "SpecialtyId" },
                values: new object[,]
                {
                    { 1, 1, "a3717562-385e-41ce-9eff-0f1b994e5548", "https://storage.cloud.google.com/healthsync/ivan-ivanov.jpg", "I am Dr. Ivan Ivanov, an orthodontist with over 10 years of experience. I earned my Doctor of Dental Medicine (DMD) degree from Sofia Medical University, where I also completed my orthodontic specialization. I have worked in various reputable dental clinics, providing treatments such as braces, clear aligners, and other advanced orthodontic procedures for patients of all ages. I focus on delivering personalized care, creating treatment plans tailored to each patient’s specific needs. I hold certifications in advanced orthodontic techniques and regularly attend courses to stay updated with the latest advancements in the field. My goal is to ensure that every patient receives the best possible outcome. Known for my compassionate approach and attention to detail, I strive to help my patients achieve healthier, more beautiful smiles. My dedication to patient satisfaction and passion for orthodontics have earned me a solid reputation in the field, making me a trusted choice for care.", 30, 1 },
                    { 2, 1, "4d650e24-6b66-41e3-8391-efab8c31a1dd", "https://storage.cloud.google.com/healthsync/maria-marinova.jpg", null, 30, 2 },
                    { 3, 1, "88cd5a7b-01d8-49b4-8688-35cd23751532", "https://storage.cloud.google.com/healthsync/aleks-kirilov.jpg", null, 15, 2 },
                    { 4, 1, "95189f02-fb1a-4700-95e3-6146b8aa8b15", "https://storage.cloud.google.com/healthsync/kiril-conev.jpg", null, 15, 3 },
                    { 5, 2, "f37b43ca-86a2-4b11-972d-5e0569f4deb3", "https://storage.cloud.google.com/healthsync/ivana-ivanova.jpg", null, 20, 4 },
                    { 6, 2, "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35", "https://storage.cloud.google.com/healthsync/monika-kirilova.jpg", null, 30, 3 },
                    { 7, 2, "78850da7-a0ff-42f3-a862-d162457910a0", null, null, 10, 1 }
                });

            migrationBuilder.InsertData(
                table: "DaysOff",
                columns: new[] { "Id", "Date", "DoctorId" },
                values: new object[] { 1, new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "Date", "DoctorId", "Rating", "Reviewer" },
                values: new object[,]
                {
                    { 1, "Dr. Ivanov went above and beyond in providing exceptional care. He took the time to listen to all my concerns, explained each step of the treatment, and made me feel at ease throughout the process. His professionalism, kindness, and dedication are truly appreciated. Highly recommended!", new DateTime(2024, 10, 14, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 5, "Aleks Petrov" },
                    { 2, "Some of my questions felt unanswered clearer explanations would be appreciated.", new DateTime(2024, 9, 19, 10, 32, 0, 0, DateTimeKind.Unspecified), 1, 2, "Maria Kostova" },
                    { 3, "Your attention and compassion made a huge difference!", new DateTime(2023, 1, 24, 19, 52, 0, 0, DateTimeKind.Unspecified), 1, 4, "Kristin Angelova" },
                    { 4, "I’d appreciate simpler language for medical terms next time.", new DateTime(2023, 1, 25, 13, 12, 0, 0, DateTimeKind.Unspecified), 1, 3, "Angel Bogdanski" },
                    { 5, "Your dedication and support mean so much—thank you!", new DateTime(2024, 7, 20, 18, 52, 0, 0, DateTimeKind.Unspecified), 1, 5, "Kosta Adamovich" },
                    { 6, "Your empathy and expertise are truly appreciated!", new DateTime(2024, 8, 4, 7, 0, 0, 0, DateTimeKind.Unspecified), 1, 5, "Kristian Ivanov" },
                    { 7, "More guidance on the next steps for my treatment would be helpful.", new DateTime(2024, 6, 15, 22, 12, 0, 0, DateTimeKind.Unspecified), 1, 2, "Viktor Terziev" },
                    { 8, "Thank you for your exceptional care and expertise!", new DateTime(2024, 4, 2, 8, 2, 0, 0, DateTimeKind.Unspecified), 2, 5, "Yordan Angelov" },
                    { 9, "I couldn't be more pleased with the level of care I received. Your attentiveness, kindness, and expertise made a world of difference.", new DateTime(2022, 12, 12, 10, 43, 0, 0, DateTimeKind.Unspecified), 2, 5, "Kristian Ivanov" }
                });

            migrationBuilder.InsertData(
                table: "WeekDays",
                columns: new[] { "Id", "Day", "DoctorId", "End", "IsWorkDay", "Start" },
                values: new object[,]
                {
                    { 1, 1, 1, new TimeSpan(0, 17, 30, 0, 0), true, new TimeSpan(0, 9, 30, 0, 0) },
                    { 2, 2, 1, new TimeSpan(0, 17, 30, 0, 0), true, new TimeSpan(0, 12, 30, 0, 0) },
                    { 3, 3, 1, new TimeSpan(0, 17, 30, 0, 0), true, new TimeSpan(0, 9, 30, 0, 0) },
                    { 4, 4, 1, new TimeSpan(0, 17, 30, 0, 0), true, new TimeSpan(0, 12, 30, 0, 0) },
                    { 5, 5, 1, new TimeSpan(0, 17, 30, 0, 0), true, new TimeSpan(0, 9, 30, 0, 0) },
                    { 6, 6, 1, new TimeSpan(0, 0, 0, 0, 0), false, new TimeSpan(0, 0, 0, 0, 0) },
                    { 7, 0, 1, new TimeSpan(0, 0, 0, 0, 0), false, new TimeSpan(0, 0, 0, 0, 0) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a550b411-e5c7-407f-aaba-35161bab127e", "4d650e24-6b66-41e3-8391-efab8c31a1dd" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a550b411-e5c7-407f-aaba-35161bab127e", "78850da7-a0ff-42f3-a862-d162457910a0" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a550b411-e5c7-407f-aaba-35161bab127e", "88cd5a7b-01d8-49b4-8688-35cd23751532" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a550b411-e5c7-407f-aaba-35161bab127e", "95189f02-fb1a-4700-95e3-6146b8aa8b15" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a550b411-e5c7-407f-aaba-35161bab127e", "a3717562-385e-41ce-9eff-0f1b994e5548" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a550b411-e5c7-407f-aaba-35161bab127e", "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a550b411-e5c7-407f-aaba-35161bab127e", "f37b43ca-86a2-4b11-972d-5e0569f4deb3" });

            migrationBuilder.DeleteData(
                table: "DaysOff",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a550b411-e5c7-407f-aaba-35161bab127e");

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
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 4);

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
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
