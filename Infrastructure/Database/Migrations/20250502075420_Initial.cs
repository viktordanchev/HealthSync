using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorSpecialties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSpecialties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SenderId = table.Column<string>(type: "text", nullable: false),
                    ReceiverId = table.Column<string>(type: "text", nullable: false),
                    DateAndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatMessages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdentityId = table.Column<string>(type: "text", nullable: false),
                    HospitalId = table.Column<int>(type: "integer", nullable: false),
                    SpecialtyId = table.Column<int>(type: "integer", nullable: false),
                    ContactEmail = table.Column<string>(type: "text", nullable: true),
                    ContactPhoneNumber = table.Column<string>(type: "text", nullable: true),
                    ImgUrl = table.Column<string>(type: "text", nullable: true),
                    Information = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_AspNetUsers_IdentityId",
                        column: x => x.IdentityId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_DoctorSpecialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "DoctorSpecialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MessageId = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageImages_ChatMessages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "ChatMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorsDaysOff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorsDaysOff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorsDaysOff_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorsMeetings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<string>(type: "text", nullable: false),
                    DateAndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorsMeetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorsMeetings_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DoctorsMeetings_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorsReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    DateAndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Reviewer = table.Column<string>(type: "text", nullable: false),
                    Comment = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorsReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorsReviews_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorsWeekDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    WeekDay = table.Column<int>(type: "integer", nullable: false),
                    IsWorkDay = table.Column<bool>(type: "boolean", nullable: false),
                    WorkDayStart = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    WorkDayEnd = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    MeetingTimeMinutes = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorsWeekDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorsWeekDays_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "87ac5db3-1586-4580-8037-ed108b66a9b1", null, "Doctor", "DOCTOR" },
                    { "bd4718fa-f25a-40ab-abce-8261cc3ea8e8", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "4d650e24-6b66-41e3-8391-efab8c31a1dd", 0, "183852b2-7fb0-404e-9d0f-01103dff2c0a", "m.marinova@mail.com", true, "Maria", "Marinova", false, null, "M.MARINOVA@MAIL.COM", "M.MARINOVA@MAIL.COM", "AQAAAAIAAYagAAAAEFoqTAw2yQ/iQMPYA2/JGHWKMsiK+gZ520dQEaSy5gr5Aa2TDec3gTnB62vZ01oulw==", null, false, "e391fc79-6ba1-4403-8610-6922f8bddd50", false, "m.marinova@mail.com" },
                    { "78850da7-a0ff-42f3-a862-d162457910a0", 0, "2122ef41-6ca1-4c94-a83f-8f6ad35b35b9", "v.yankova@mail.com", true, "Vanya", "Yankova", false, null, "V.YANKOVA@MAIL.COM", "V.YANKOVA@MAIL.COM", "AQAAAAIAAYagAAAAEPNjNVeofJHRv22jj+Q61Xvfhg/BsIFZ5CclLEMqKM46E9Fa0XVdGfU7xOzW5Y/39Q==", null, false, "35832bdc-5177-493e-bf0c-e5cba53fa8d8", false, "v.yankova@mail.com" },
                    { "88cd5a7b-01d8-49b4-8688-35cd23751532", 0, "25453aef-9d58-4757-82cc-5fd3d9ad0714", "a.kirilov@mail.com", true, "Aleks", "Kirilov", false, null, "A.KIRILOV@MAIL.COM", "A.KIRILOV@MAIL.COM", "AQAAAAIAAYagAAAAEKLpfjbPr3xcQ4OT+ZO2o+TbDz6Aoc98ZLYP6erZMn5i6ojr54eBCBxXLOdTqHy3Xw==", null, false, "76d3b694-4c92-488e-b640-bd7d087a9186", false, "a.kirilov@mail.com" },
                    { "95189f02-fb1a-4700-95e3-6146b8aa8b15", 0, "3add8d92-daa8-4f38-ba0c-43507fc52f8c", "k.conev@mail.com", true, "Kiril", "Conev", false, null, "K.CONEV@MAIL.COM", "K.CONEV@MAIL.COM", "AQAAAAIAAYagAAAAEMhj5cBPrVfJDv0QI4lSSae8mxL0wUWpaBRz3VNjcXVHghq4EPZ8D8kRdlj7HV9/Eg==", null, false, "fed8dda5-d07e-4176-8031-f460e3ffd145", false, "k.conev@mail.com" },
                    { "a3717562-385e-41ce-9eff-0f1b994e5548", 0, "5e9b9eb9-0ed4-4051-af7d-0fea842041c3", "i.ivanov@mail.com", true, "Ivan", "Ivanov", false, null, "I.IVANOV@MAIL.COM", "I.IVANOV@MAIL.COM", "AQAAAAIAAYagAAAAEPToz3vdvu26mAvn/hENl9CrOqqBTiGnJ/JVvp3JViR0h6hcOGPdGR1LUtj8E5zXwA==", null, false, "365be671-bf5e-4b7e-840a-1947b97514da", false, "i.ivanov@mail.com" },
                    { "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35", 0, "699c6705-0fe3-42af-8381-5568c54086db", "m.kirilova@mail.com", true, "Monika", "Kirilova", false, null, "M.KIRILOVA@MAIL.COM", "M.KIRILOVA@MAIL.COM", "AQAAAAIAAYagAAAAELvPn5upjUlzESJSYJStjjqdfXQHyKuH/S8AQGJRtW1lXpR1d/tPcCSZTVE2BrGK1Q==", null, false, "1beb54dc-b91e-42da-9b45-d1150c7d597e", false, "m.kirilova@mail.com" },
                    { "f37b43ca-86a2-4b11-972d-5e0569f4deb3", 0, "cca9dd8b-2339-4fd9-8a19-8e845b259e0e", "i.ivanova@mail.com", true, "Ivana", "Ivanova", false, null, "I.IVANOVA@MAIL.COM", "I.IVANOVA@MAIL.COM", "AQAAAAIAAYagAAAAEEcPmawhhasIdD+BeSOA/V4YK2NYMNURTMj6n3ufrLJ5zCqs2rtCQzkhyLJencLO1w==", null, false, "4f5d4e00-fed0-4cb3-b60e-eb65f444a602", false, "i.ivanova@mail.com" }
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
                    { "87ac5db3-1586-4580-8037-ed108b66a9b1", "4d650e24-6b66-41e3-8391-efab8c31a1dd" },
                    { "87ac5db3-1586-4580-8037-ed108b66a9b1", "78850da7-a0ff-42f3-a862-d162457910a0" },
                    { "87ac5db3-1586-4580-8037-ed108b66a9b1", "88cd5a7b-01d8-49b4-8688-35cd23751532" },
                    { "87ac5db3-1586-4580-8037-ed108b66a9b1", "95189f02-fb1a-4700-95e3-6146b8aa8b15" },
                    { "87ac5db3-1586-4580-8037-ed108b66a9b1", "a3717562-385e-41ce-9eff-0f1b994e5548" },
                    { "87ac5db3-1586-4580-8037-ed108b66a9b1", "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35" },
                    { "87ac5db3-1586-4580-8037-ed108b66a9b1", "f37b43ca-86a2-4b11-972d-5e0569f4deb3" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "ContactEmail", "ContactPhoneNumber", "HospitalId", "IdentityId", "ImgUrl", "Information", "SpecialtyId" },
                values: new object[,]
                {
                    { 1, null, null, 1, "a3717562-385e-41ce-9eff-0f1b994e5548", "https://healthsyncstorage.blob.core.windows.net/profile-images/a3717562-385e-41ce-9eff-0f1b994e5548.jpg", "I am Dr. Ivan Ivanov, an orthodontist with over 10 years of experience. I earned my Doctor of Dental Medicine (DMD) degree from Sofia Medical University, where I also completed my orthodontic specialization. I have worked in various reputable dental clinics, providing treatments such as braces, clear aligners, and other advanced orthodontic procedures for patients of all ages. I focus on delivering personalized care, creating treatment plans tailored to each patient’s specific needs. I hold certifications in advanced orthodontic techniques and regularly attend courses to stay updated with the latest advancements in the field. My goal is to ensure that every patient receives the best possible outcome. Known for my compassionate approach and attention to detail, I strive to help my patients achieve healthier, more beautiful smiles. My dedication to patient satisfaction and passion for orthodontics have earned me a solid reputation in the field, making me a trusted choice for care.", 1 },
                    { 2, null, null, 1, "4d650e24-6b66-41e3-8391-efab8c31a1dd", "https://healthsyncstorage.blob.core.windows.net/profile-images/4d650e24-6b66-41e3-8391-efab8c31a1dd.jpg", "I graduated from the Medical University of Sofia and have been practicing medicine for over 12 years. Throughout my career, I have focused on internal medicine, always striving to offer thorough and personalized care. I believe in building strong relationships with my patients to ensure long-term health and well-being.", 2 },
                    { 3, null, null, 1, "88cd5a7b-01d8-49b4-8688-35cd23751532", "https://healthsyncstorage.blob.core.windows.net/profile-images/88cd5a7b-01d8-49b4-8688-35cd23751532.jpg", "I am a dedicated endocrinologist with a passion for helping patients manage their hormonal health. I graduated from Sofia Medical University and completed my residency in endocrinology at the same institution. With over 8 years of experience, I specialize in treating conditions such as diabetes, thyroid disorders, and adrenal gland issues. My approach to patient care is holistic, focusing on both medical treatment and lifestyle modifications to achieve optimal health outcomes. I am committed to staying updated with the latest advancements in endocrinology to provide the best care possible.", 2 },
                    { 4, null, null, 1, "95189f02-fb1a-4700-95e3-6146b8aa8b15", "https://healthsyncstorage.blob.core.windows.net/profile-images/95189f02-fb1a-4700-95e3-6146b8aa8b15.jpg", "I am a cardiologist with a strong commitment to patient care and education. I graduated from Sofia Medical University and completed my residency in cardiology at the same institution. With over 10 years of experience, I specialize in diagnosing and treating various heart conditions, including hypertension, coronary artery disease, and heart failure. My approach to patient care emphasizes prevention and lifestyle modifications, and I work closely with my patients to develop personalized treatment plans. I am dedicated to staying current with the latest advancements in cardiology to provide the best possible care.", 3 },
                    { 5, null, null, 2, "f37b43ca-86a2-4b11-972d-5e0569f4deb3", "https://healthsyncstorage.blob.core.windows.net/profile-images/f37b43ca-86a2-4b11-972d-5e0569f4deb3.jpg", "I am a neurologist with a passion for understanding the complexities of the human brain and nervous system. I graduated from Sofia Medical University and completed my residency in neurology at the same institution. With over 7 years of experience, I specialize in diagnosing and treating neurological disorders such as epilepsy, multiple sclerosis, and migraines. My approach to patient care is comprehensive, focusing on both medical treatment and lifestyle modifications to improve overall health. I am committed to providing compassionate care and staying updated with the latest advancements in neurology.", 4 },
                    { 6, null, null, 2, "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35", "https://healthsyncstorage.blob.core.windows.net/profile-images/d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35.jpg", "I am a cardiologist with a strong commitment to patient care and education. I graduated from Sofia Medical University and completed my residency in cardiology at the same institution. With over 10 years of experience, I specialize in diagnosing and treating various heart conditions, including hypertension, coronary artery disease, and heart failure. My approach to patient care emphasizes prevention and lifestyle modifications, and I work closely with my patients to develop personalized treatment plans. I am dedicated to staying current with the latest advancements in cardiology to provide the best possible care.", 3 },
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
                    { 9, "I couldn't be more pleased with the level of care I received. Your attentiveness, kindness, and expertise made a world of difference.", new DateTime(2022, 12, 12, 10, 43, 0, 0, DateTimeKind.Unspecified), 2, 5, "Kristian Ivanov" },
                    { 10, "Thank you for your exceptional care and expertise!", new DateTime(2020, 7, 12, 16, 42, 0, 0, DateTimeKind.Unspecified), 3, 5, "Kristian Ivanov" }
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
                    { 7, 1, false, 0, 0, new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0) },
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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ReceiverId",
                table: "ChatMessages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderId",
                table: "ChatMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_HospitalId",
                table: "Doctors",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_IdentityId",
                table: "Doctors",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecialtyId",
                table: "Doctors",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsDaysOff_DoctorId",
                table: "DoctorsDaysOff",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsMeetings_DoctorId",
                table: "DoctorsMeetings",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsMeetings_PatientId",
                table: "DoctorsMeetings",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsReviews_DoctorId",
                table: "DoctorsReviews",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsWeekDays_DoctorId",
                table: "DoctorsWeekDays",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageImages_MessageId",
                table: "MessageImages",
                column: "MessageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DoctorsDaysOff");

            migrationBuilder.DropTable(
                name: "DoctorsMeetings");

            migrationBuilder.DropTable(
                name: "DoctorsReviews");

            migrationBuilder.DropTable(
                name: "DoctorsWeekDays");

            migrationBuilder.DropTable(
                name: "MessageImages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "DoctorSpecialties");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
