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
                values: new object[,]
                {
                    { "a550b411-e5c7-407f-aaba-35161bab127e", null, "Doctor", "DOCTOR" },
                    { "bd4718fa-f25a-40ab-abce-8261cc3ea8e8", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "4d650e24-6b66-41e3-8391-efab8c31a1dd", 0, "5b2b6097-5a16-46af-964b-831197214c28", "m.marinova@mail.com", true, "Maria", "Marinova", false, null, "M.MARINOVA@MAIL.COM", "M.MARINOVA@MAIL.COM", "AQAAAAIAAYagAAAAEEvB+Dmvwm1ZSzye+Ga/CQu1d6OlJpo3Qdsc4uWrGxQs2vqxwQ4Ud5xizgXfqltLPw==", null, false, "2e6ee4b4-80cd-4737-92a9-e45b4fb5045c", false, "m.marinova@mail.com" },
                    { "78850da7-a0ff-42f3-a862-d162457910a0", 0, "e00a7e24-0158-4b96-8579-22208ed1254c", "v.yankova@mail.com", true, "Vanya", "Yankova", false, null, "V.YANKOVA@MAIL.COM", "V.YANKOVA@MAIL.COM", "AQAAAAIAAYagAAAAEH23LGBuMK08x0DCtZ7c7161v11MT0Cyamb651IdYioUUGwLZ51wqAzI/F3OstraMQ==", null, false, "c22fc5dd-a43d-43b9-b565-a45b2e86adcd", false, "v.yankova@mail.com" },
                    { "88cd5a7b-01d8-49b4-8688-35cd23751532", 0, "a832afea-ac66-4f1c-b08c-5ed4b4ccf068", "a.kirilov@mail.com", true, "Aleks", "Kirilov", false, null, "A.KIRILOV@MAIL.COM", "A.KIRILOV@MAIL.COM", "AQAAAAIAAYagAAAAEGnUb+xGo8vSPBOmI0o9EcCTIpo4+BYOGf6D2ILsHX2rCS/J3PoCjv1ieQrR3dQJ3w==", null, false, "4098681a-0056-4e4f-afef-119835badcb8", false, "a.kirilov@mail.com" },
                    { "95189f02-fb1a-4700-95e3-6146b8aa8b15", 0, "f21788b4-b6c5-442e-938f-24b58097be76", "k.conev@mail.com", true, "Kiril", "Conev", false, null, "K.CONEV@MAIL.COM", "K.CONEV@MAIL.COM", "AQAAAAIAAYagAAAAELJd3CrdqVfbjqdiLG7mPdNACV2waFa/vzuDlnwd77lvQ2ysvrBIA886TN0azYjnww==", null, false, "5e53f7cf-6da3-4847-bcf8-49677fe95761", false, "k.conev@mail.com" },
                    { "a3717562-385e-41ce-9eff-0f1b994e5548", 0, "98c8a1ff-c3d4-4b8a-a804-65c53d093570", "i.ivanov@mail.com", true, "Ivan", "Ivanov", false, null, "I.IVANOV@MAIL.COM", "I.IVANOV@MAIL.COM", "AQAAAAIAAYagAAAAEONscRxS2HGl9ZFeS8cXJQ5INbJM/TU9IT6WehUQwIA844SEAF3eKw4dlaBQ5loigA==", null, false, "63c940d2-2b96-4bca-ae4a-7a9e73918aef", false, "i.ivanov@mail.com" },
                    { "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35", 0, "312bc87a-9a23-40d2-8a6b-10f5220f7db0", "m.kirilova@mail.com", true, "Monika", "Kirilova", false, null, "M.KIRILOVA@MAIL.COM", "M.KIRILOVA@MAIL.COM", "AQAAAAIAAYagAAAAELZYr93vnjBFk5d9Whuq+uWjJL2N3tv83NF1o4JmtLxJbq+B6TBMY84fAu9dFHrmGw==", null, false, "3e5e55d1-d3d6-4fe6-812e-a7cdc1343ef9", false, "m.kirilova@mail.com" },
                    { "f37b43ca-86a2-4b11-972d-5e0569f4deb3", 0, "bca56f75-fde0-4003-b826-ea18a45f686a", "i.ivanova@mail.com", true, "Ivana", "Ivanova", false, null, "I.IVANOVA@MAIL.COM", "I.IVANOVA@MAIL.COM", "AQAAAAIAAYagAAAAEALeuoXqsBaGlB2WD+xyyn9pl6EdCJLvt2j9Q9IntZc7cjFH2uJHQmd4LSD22axdww==", null, false, "dc5e9d74-e9f1-47e1-8c5d-a3417840b9ba", false, "i.ivanova@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "DoctorSpecialties",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Orthodontist" },
                    { 2, "Endocrinologist" },
                    { 3, "Cardiologist" },
                    { 4, "Neurologist" }
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
                columns: new[] { "Id", "ContactEmail", "ContactPhoneNumber", "HospitalId", "IdentityId", "ImgUrl", "Information", "SpecialtyId" },
                values: new object[,]
                {
                    { 1, null, null, 1, "a3717562-385e-41ce-9eff-0f1b994e5548", "https://storage.cloud.google.com/healthsync/profile-images/Ivan-Ivanov.jpg", "I am Dr. Ivan Ivanov, an orthodontist with over 10 years of experience. I earned my Doctor of Dental Medicine (DMD) degree from Sofia Medical University, where I also completed my orthodontic specialization. I have worked in various reputable dental clinics, providing treatments such as braces, clear aligners, and other advanced orthodontic procedures for patients of all ages. I focus on delivering personalized care, creating treatment plans tailored to each patient’s specific needs. I hold certifications in advanced orthodontic techniques and regularly attend courses to stay updated with the latest advancements in the field. My goal is to ensure that every patient receives the best possible outcome. Known for my compassionate approach and attention to detail, I strive to help my patients achieve healthier, more beautiful smiles. My dedication to patient satisfaction and passion for orthodontics have earned me a solid reputation in the field, making me a trusted choice for care.", 1 },
                    { 2, null, null, 1, "4d650e24-6b66-41e3-8391-efab8c31a1dd", "https://storage.cloud.google.com/healthsync/profile-images/Maria-Marinova.jpg", null, 2 },
                    { 3, null, null, 1, "88cd5a7b-01d8-49b4-8688-35cd23751532", "https://storage.cloud.google.com/healthsync/profile-images/Aleks-Kirilov.jpg", null, 2 },
                    { 4, null, null, 1, "95189f02-fb1a-4700-95e3-6146b8aa8b15", "https://storage.cloud.google.com/healthsync/profile-images/Kiril-Conev.jpg", null, 3 },
                    { 5, null, null, 2, "f37b43ca-86a2-4b11-972d-5e0569f4deb3", "https://storage.cloud.google.com/healthsync/profile-images/Ivana-Ivanova.jpg", null, 4 },
                    { 6, null, null, 2, "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35", "https://storage.cloud.google.com/healthsync/profile-images/Monika-Kirilova.jpg", null, 3 },
                    { 7, null, null, 2, "78850da7-a0ff-42f3-a862-d162457910a0", null, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "DoctorsDaysOff",
                columns: new[] { "Id", "Day", "DoctorId", "Month" },
                values: new object[] { 1, 25, 1, 12 });

            migrationBuilder.InsertData(
                table: "DoctorsReviews",
                columns: new[] { "Id", "Comment", "DateAndTime", "DoctorId", "Rating", "Reviewer" },
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
                table: "DoctorsWeekDays",
                columns: new[] { "Id", "DoctorId", "IsWorkDay", "MeetingTimeMinutes", "WeekDay", "WorkDayEnd", "WorkDayStart" },
                values: new object[,]
                {
                    { 1, 1, true, 30, 1, new TimeOnly(17, 30, 0), new TimeOnly(9, 30, 0) },
                    { 2, 1, true, 30, 2, new TimeOnly(17, 30, 0), new TimeOnly(12, 30, 0) },
                    { 3, 1, true, 30, 3, new TimeOnly(17, 30, 0), new TimeOnly(9, 30, 0) },
                    { 4, 1, true, 30, 4, new TimeOnly(17, 30, 0), new TimeOnly(12, 30, 0) },
                    { 5, 1, true, 30, 5, new TimeOnly(17, 30, 0), new TimeOnly(9, 30, 0) },
                    { 6, 1, false, 0, 6, new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0) },
                    { 7, 1, false, 0, 0, new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd4718fa-f25a-40ab-abce-8261cc3ea8e8");

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
                table: "DoctorsDaysOff",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DoctorsReviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DoctorsReviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DoctorsReviews",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DoctorsReviews",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DoctorsReviews",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DoctorsReviews",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DoctorsReviews",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DoctorsReviews",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DoctorsReviews",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
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
                table: "DoctorSpecialties",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DoctorSpecialties",
                keyColumn: "Id",
                keyValue: 4);

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
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d650e24-6b66-41e3-8391-efab8c31a1dd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a3717562-385e-41ce-9eff-0f1b994e5548");

            migrationBuilder.DeleteData(
                table: "DoctorSpecialties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DoctorSpecialties",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
