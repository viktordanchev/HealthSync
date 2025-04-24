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
            migrationBuilder.DeleteData(
                table: "DoctorsReviews",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d650e24-6b66-41e3-8391-efab8c31a1dd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b3671bf-0eee-4404-976b-81b66565e8e9", "AQAAAAIAAYagAAAAEAmhPL3RQauJ01sdJzWeZruzsXUg23T24mFHj6P54KI3otJEDqbN2nR8LKlWEZRahQ==", "f26e9281-f2b8-413f-a4ad-6d54539bebb4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "78850da7-a0ff-42f3-a862-d162457910a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eec4bdf5-e7c9-43c7-85cc-6af7344bd61a", "AQAAAAIAAYagAAAAELOfy1D3MceC1/EG9lPSbs1KSDIy9siNmzBAeq18QDzefbUJbNuieH+cA+dfzKpvTA==", "888b6567-1917-4cf0-a347-b20a1f11b8ef" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "88cd5a7b-01d8-49b4-8688-35cd23751532",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "816de44d-2c5a-46ad-882b-b2d93f6ab304", "AQAAAAIAAYagAAAAEAFK3mykH7BcH0gU2J3UtxB5a2AkZn1VXbI5LGQCf1Sj/QxHT4LMPzNudKjnXuI7yg==", "6386abde-c145-4d81-b5b6-0990dd8b4daa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "95189f02-fb1a-4700-95e3-6146b8aa8b15",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a2fa5f4c-c97d-4755-9f82-b00d4a2f7247", "AQAAAAIAAYagAAAAEO+bY8kYANmGz9V0ouiViduFxJFk+InYwzi/oEB5SMMAYUydEQNB9REMM8Lvnv+OkA==", "c45baee9-d372-4d8b-82ec-fc5b6b90ef42" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a3717562-385e-41ce-9eff-0f1b994e5548",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3b1d650-566c-4238-936d-900f6ca4a275", "AQAAAAIAAYagAAAAEJMe/1Dh4DiwGXA1MM85UClCjUyuzd63G0sND/UNuQlUqxM02vitPVkag3n/qdjW5A==", "e897f6d8-3e7e-4901-88bd-f4668e9f6ec9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c9094bf9-f4db-42df-8d58-b3aa763ee379", "AQAAAAIAAYagAAAAEDYzD9LWbBrsklghug8MfYLsQlVmbdbTgfBrGhxXJQ+QpJ3MWLTXbQh95EkfSNaVsA==", "ca94bfe6-40cf-474d-befc-82d3888c1d0b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f37b43ca-86a2-4b11-972d-5e0569f4deb3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ed91c64-7cf5-412b-8a41-ab59eb3efb36", "AQAAAAIAAYagAAAAECTsGD65J2MskugUu/BFpTIt5B/bl9e4TkBEt3OxmzHBa2oMVT7bguGDIfwy1dkTyg==", "bd410246-87ed-4e0d-8e87-4fd8c0456ee5" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImgUrl",
                value: "https://healthsyncstorage.blob.core.windows.net/profile-images/a3717562-385e-41ce-9eff-0f1b994e5548.jpg");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImgUrl", "Information" },
                values: new object[] { "https://healthsyncstorage.blob.core.windows.net/profile-images/4d650e24-6b66-41e3-8391-efab8c31a1dd.jpg", "I graduated from the Medical University of Sofia and have been practicing medicine for over 12 years. Throughout my career, I have focused on internal medicine, always striving to offer thorough and personalized care. I believe in building strong relationships with my patients to ensure long-term health and well-being." });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ImgUrl", "Information" },
                values: new object[] { "https://healthsyncstorage.blob.core.windows.net/profile-images/88cd5a7b-01d8-49b4-8688-35cd23751532.jpg", "I am a dedicated endocrinologist with a passion for helping patients manage their hormonal health. I graduated from Sofia Medical University and completed my residency in endocrinology at the same institution. With over 8 years of experience, I specialize in treating conditions such as diabetes, thyroid disorders, and adrenal gland issues. My approach to patient care is holistic, focusing on both medical treatment and lifestyle modifications to achieve optimal health outcomes. I am committed to staying updated with the latest advancements in endocrinology to provide the best care possible." });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ImgUrl", "Information" },
                values: new object[] { "https://healthsyncstorage.blob.core.windows.net/profile-images/95189f02-fb1a-4700-95e3-6146b8aa8b15.jpg", "I am a cardiologist with a strong commitment to patient care and education. I graduated from Sofia Medical University and completed my residency in cardiology at the same institution. With over 10 years of experience, I specialize in diagnosing and treating various heart conditions, including hypertension, coronary artery disease, and heart failure. My approach to patient care emphasizes prevention and lifestyle modifications, and I work closely with my patients to develop personalized treatment plans. I am dedicated to staying current with the latest advancements in cardiology to provide the best possible care." });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ImgUrl", "Information" },
                values: new object[] { "https://healthsyncstorage.blob.core.windows.net/profile-images/f37b43ca-86a2-4b11-972d-5e0569f4deb3.jpg", "I am a neurologist with a passion for understanding the complexities of the human brain and nervous system. I graduated from Sofia Medical University and completed my residency in neurology at the same institution. With over 7 years of experience, I specialize in diagnosing and treating neurological disorders such as epilepsy, multiple sclerosis, and migraines. My approach to patient care is comprehensive, focusing on both medical treatment and lifestyle modifications to improve overall health. I am committed to providing compassionate care and staying updated with the latest advancements in neurology." });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ImgUrl", "Information" },
                values: new object[] { "https://healthsyncstorage.blob.core.windows.net/profile-images/d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35.jpg", "I am a cardiologist with a strong commitment to patient care and education. I graduated from Sofia Medical University and completed my residency in cardiology at the same institution. With over 10 years of experience, I specialize in diagnosing and treating various heart conditions, including hypertension, coronary artery disease, and heart failure. My approach to patient care emphasizes prevention and lifestyle modifications, and I work closely with my patients to develop personalized treatment plans. I am dedicated to staying current with the latest advancements in cardiology to provide the best possible care." });

            migrationBuilder.InsertData(
                table: "DoctorsReviews",
                columns: new[] { "Id", "Comment", "DateAndTime", "DoctorId", "Rating", "Reviewer" },
                values: new object[] { 10, "Thank you for your exceptional care and expertise!", new DateTime(2020, 7, 12, 16, 42, 0, 0, DateTimeKind.Unspecified), 3, 5, "Kristian Ivanov" });

            migrationBuilder.InsertData(
                table: "DoctorsWeekDays",
                columns: new[] { "Id", "DoctorId", "IsWorkDay", "MeetingTimeMinutes", "WeekDay", "WorkDayEnd", "WorkDayStart" },
                values: new object[,]
                {
                    { 8, 2, true, 15, 1, new TimeOnly(16, 0, 0), new TimeOnly(10, 30, 0) },
                    { 9, 2, true, 15, 2, new TimeOnly(16, 0, 0), new TimeOnly(10, 30, 0) },
                    { 10, 2, true, 15, 3, new TimeOnly(16, 0, 0), new TimeOnly(11, 30, 0) },
                    { 11, 2, true, 15, 4, new TimeOnly(16, 0, 0), new TimeOnly(11, 30, 0) },
                    { 12, 2, true, 15, 5, new TimeOnly(16, 0, 0), new TimeOnly(11, 30, 0) },
                    { 13, 2, true, 15, 6, new TimeOnly(16, 0, 0), new TimeOnly(11, 30, 0) },
                    { 14, 2, false, 0, 0, new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0) },
                    { 15, 3, true, 10, 1, new TimeOnly(17, 0, 0), new TimeOnly(9, 0, 0) },
                    { 16, 3, true, 10, 2, new TimeOnly(17, 0, 0), new TimeOnly(9, 0, 0) },
                    { 17, 3, true, 10, 3, new TimeOnly(17, 0, 0), new TimeOnly(9, 0, 0) },
                    { 18, 3, false, 0, 4, new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0) },
                    { 19, 3, false, 0, 5, new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0) },
                    { 20, 3, false, 0, 6, new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0) },
                    { 21, 3, false, 0, 0, new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0) },
                    { 22, 4, true, 45, 1, new TimeOnly(22, 0, 0), new TimeOnly(10, 0, 0) },
                    { 23, 4, true, 45, 2, new TimeOnly(22, 0, 0), new TimeOnly(10, 0, 0) },
                    { 24, 4, true, 45, 3, new TimeOnly(22, 0, 0), new TimeOnly(10, 0, 0) },
                    { 25, 4, true, 45, 4, new TimeOnly(22, 0, 0), new TimeOnly(10, 0, 0) },
                    { 26, 4, true, 45, 5, new TimeOnly(22, 0, 0), new TimeOnly(10, 0, 0) },
                    { 27, 4, false, 0, 6, new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0) },
                    { 28, 4, false, 0, 0, new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0) },
                    { 29, 5, false, 0, 1, new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0) },
                    { 30, 5, true, 15, 2, new TimeOnly(6, 0, 0), new TimeOnly(23, 0, 0) },
                    { 31, 5, true, 15, 3, new TimeOnly(6, 0, 0), new TimeOnly(23, 0, 0) },
                    { 32, 5, true, 15, 4, new TimeOnly(17, 0, 0), new TimeOnly(10, 0, 0) },
                    { 33, 5, true, 15, 5, new TimeOnly(17, 0, 0), new TimeOnly(10, 0, 0) },
                    { 34, 5, false, 0, 6, new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0) },
                    { 35, 5, false, 0, 0, new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0) },
                    { 36, 6, true, 60, 1, new TimeOnly(20, 30, 0), new TimeOnly(10, 0, 0) },
                    { 37, 6, true, 60, 2, new TimeOnly(20, 30, 0), new TimeOnly(10, 0, 0) },
                    { 38, 6, true, 60, 3, new TimeOnly(20, 30, 0), new TimeOnly(10, 0, 0) },
                    { 39, 6, true, 60, 4, new TimeOnly(20, 30, 0), new TimeOnly(12, 0, 0) },
                    { 40, 6, true, 60, 5, new TimeOnly(20, 30, 0), new TimeOnly(12, 0, 0) },
                    { 41, 6, false, 0, 6, new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0) },
                    { 42, 6, false, 0, 0, new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DoctorsReviews",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "DoctorsWeekDays",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d650e24-6b66-41e3-8391-efab8c31a1dd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0d00ed0e-4f10-42a5-9e1f-aace562e5b08", "AQAAAAIAAYagAAAAEM/ObRSIaOUtuVIy+mZW7uPTugAJqq1H9Ps/ZSzLXzmWYwrHO4YCR3QxKBl1qg6jrA==", "1978a642-3f8b-4487-b463-3713ba0215da" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "78850da7-a0ff-42f3-a862-d162457910a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "486a5e98-4e4e-4420-b891-079c715c4b93", "AQAAAAIAAYagAAAAEOSQ+MZW2XJUwLUN0OH3UGtR1Asjh+e4MjCstHL8Tec8BCkHBUlapSh9RP3QLsXSzQ==", "39055533-3038-4f0f-97de-fa0e3ee6e726" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "88cd5a7b-01d8-49b4-8688-35cd23751532",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ca470e85-7a74-494a-a4df-409127b71ba6", "AQAAAAIAAYagAAAAEFNXJeH5+IGixemPR2LPQZ7/XYMwzKRVUy2+65QaBevoVnV0y8VUVurSMfwbcKSUOQ==", "f4df6499-6872-4340-b060-b1ecf694c241" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "95189f02-fb1a-4700-95e3-6146b8aa8b15",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c22405ac-fd59-47b7-8fc8-aebf8800c00b", "AQAAAAIAAYagAAAAEIZFLWBU0GBfLa5GICwHNk3BlKUD0mpmaOlcqZV3yzPDfXyD2xXxSja3U7LCpxLBxQ==", "dd9fae9c-3a67-48f6-a653-79e01bc76573" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a3717562-385e-41ce-9eff-0f1b994e5548",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "995069ff-c10d-402d-bfd6-75058d99dddb", "AQAAAAIAAYagAAAAENAaVT2lz9YwLOW8Q0It3oYs9tb6tH/OSv6bcQ1614zndTZN/mgHk31XBYtNvk9kUQ==", "8aed1cc0-deb1-4107-aecf-2d7407a3c618" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c082968f-4898-4ee0-a302-b9a47da035ba", "AQAAAAIAAYagAAAAEA3j5DqCUBZNbq5KpwIhoWJFfKwjd05MCzfggMehSSr7GcnvIrQlyEL/mlnuNuzSQw==", "2233b87b-051f-4165-982d-8b51851c9a42" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f37b43ca-86a2-4b11-972d-5e0569f4deb3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6118dee0-7bf8-4d78-b9dc-b98d7ad26e12", "AQAAAAIAAYagAAAAEJ3oIqB21qLJ6Mvg6scuYDNO2vC1tNPwViruXzZJSsQxMjknwwyYRc0fSM6PnhwDKA==", "e241922a-5eb1-4e93-8b90-60e3ae1b9b63" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImgUrl",
                value: "https://healthsyncstorage.blob.core.windows.net/profile-images/Ivan-Ivanov.jpg");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImgUrl", "Information" },
                values: new object[] { "https://healthsyncstorage.blob.core.windows.net/profile-images/Maria-Marinova.jpg", null });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ImgUrl", "Information" },
                values: new object[] { "https://healthsyncstorage.blob.core.windows.net/profile-images/Aleks-Kirilov.jpg", null });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ImgUrl", "Information" },
                values: new object[] { "https://healthsyncstorage.blob.core.windows.net/profile-images/Kiril-Conev.jpg", null });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ImgUrl", "Information" },
                values: new object[] { "https://healthsyncstorage.blob.core.windows.net/profile-images/Ivana-Ivanova.jpg", null });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ImgUrl", "Information" },
                values: new object[] { "https://healthsyncstorage.blob.core.windows.net/profile-images/Monika-Kirilova.jpg", null });

            migrationBuilder.InsertData(
                table: "DoctorsReviews",
                columns: new[] { "Id", "Comment", "DateAndTime", "DoctorId", "Rating", "Reviewer" },
                values: new object[] { 7, "More guidance on the next steps for my treatment would be helpful.", new DateTime(2024, 6, 15, 22, 12, 0, 0, DateTimeKind.Unspecified), 1, 2, "Viktor Terziev" });
        }
    }
}
