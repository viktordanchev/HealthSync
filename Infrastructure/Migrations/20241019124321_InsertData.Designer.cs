﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(HealthSyncDbContext))]
    [Migration("20241019124321_InsertData")]
    partial class InsertData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "a3717562-385e-41ce-9eff-0f1b994e5548",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "6146fb8a-5d10-45b0-9c98-203713b46f66",
                            Email = "i.ivanov@mail.com",
                            EmailConfirmed = true,
                            FirstName = "Ivan",
                            LastName = "Ivanov",
                            LockoutEnabled = false,
                            NormalizedEmail = "I.IVANOV@MAIL.COM",
                            NormalizedUserName = "I.IVANOV@MAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEC1BbpUF0X6tRAyE+oJ8pmv1q7ARqewo72AcQxJBDIRd5bcni0BupO/uzbrc6ncPyQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "3a841945-d76e-4cde-9e93-bcdd4a98f0f8",
                            TwoFactorEnabled = false,
                            UserName = "i.ivanov@mail.com"
                        },
                        new
                        {
                            Id = "4d650e24-6b66-41e3-8391-efab8c31a1dd",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b3f0fdd5-a8c9-4b4c-ac0f-41b5cba2bf7f",
                            Email = "m.marinova@mail.com",
                            EmailConfirmed = true,
                            FirstName = "Maria",
                            LastName = "Marinova",
                            LockoutEnabled = false,
                            NormalizedEmail = "M.MARINOVA@MAIL.COM",
                            NormalizedUserName = "M.MARINOVA@MAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEGszcGyu/MOPkT5dQ/rQvpQ2pmpQZQIs3vuiFWrFBZ8KONJWYKCKGFIdQFMdAoBcyA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "6672184a-89b8-447e-ac7d-56aac7869f27",
                            TwoFactorEnabled = false,
                            UserName = "m.marinova@mail.com"
                        },
                        new
                        {
                            Id = "88cd5a7b-01d8-49b4-8688-35cd23751532",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "6ae91b3b-a041-45e6-a7cb-f6d474bdb903",
                            Email = "a.kirilov@mail.com",
                            EmailConfirmed = true,
                            FirstName = "Aleks",
                            LastName = "Kirilov",
                            LockoutEnabled = false,
                            NormalizedEmail = "A.KIRILOV@MAIL.COM",
                            NormalizedUserName = "A.KIRILOV@MAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEFZEtfr6Ly1MnyZX9Nfzwlynznc28Klq9lFvstzhtH4PK1TPj3YWREFdqS8s4JOBsg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "0df97eca-4ff4-4c75-a2af-eddaea08e6ce",
                            TwoFactorEnabled = false,
                            UserName = "a.kirilov@mail.com"
                        },
                        new
                        {
                            Id = "95189f02-fb1a-4700-95e3-6146b8aa8b15",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "48633bd4-4d5b-4a6d-88f7-39fe798fc2d1",
                            Email = "k.conev@mail.com",
                            EmailConfirmed = true,
                            FirstName = "Kiril",
                            LastName = "Conev",
                            LockoutEnabled = false,
                            NormalizedEmail = "K.CONEV@MAIL.COM",
                            NormalizedUserName = "K.CONEV@MAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEH5CtuMzF4xW8+bbPt95ABhTxZ7H3UpQFOpCJn+MKzWW9B0Tp6cCJawKMnRXU9Gemw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "2a8a036d-5ab4-4825-a7cf-8a73a448e633",
                            TwoFactorEnabled = false,
                            UserName = "k.conev@mail.com"
                        },
                        new
                        {
                            Id = "f37b43ca-86a2-4b11-972d-5e0569f4deb3",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "93f54c36-c2e1-420a-ae85-d79df02de76c",
                            Email = "i.ivanova@mail.com",
                            EmailConfirmed = true,
                            FirstName = "Ivana",
                            LastName = "Ivanova",
                            LockoutEnabled = false,
                            NormalizedEmail = "I.IVANOVA@MAIL.COM",
                            NormalizedUserName = "I.IVANOVA@MAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEFs+129MkzvpkbWQnxg5I8SskbbUj/mMLicfYxZ/5t0bR9pMyBMbg+aBTvv4cmwrtg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "77dc271b-dc66-4cad-b038-cbf6c34b38af",
                            TwoFactorEnabled = false,
                            UserName = "i.ivanova@mail.com"
                        },
                        new
                        {
                            Id = "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f3316017-718c-42af-bcb5-5b3820b476cc",
                            Email = "m.kirilova@mail.com",
                            EmailConfirmed = true,
                            FirstName = "Monika",
                            LastName = "Kirilova",
                            LockoutEnabled = false,
                            NormalizedEmail = "M.KIRILOVA@MAIL.COM",
                            NormalizedUserName = "M.KIRILOVA@MAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEH5jRIZ5k8QJZH9jsWlWFOVaRrOPfjUoPqXuZuGiQSNTeKU/hm00JEbDme0OzgEFXg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "4d4f0611-d7df-4698-8aa5-77736075f681",
                            TwoFactorEnabled = false,
                            UserName = "m.kirilova@mail.com"
                        },
                        new
                        {
                            Id = "78850da7-a0ff-42f3-a862-d162457910a0",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d8bc4032-ee30-4edf-80aa-8f7fce43e5dd",
                            Email = "v.yankova@mail.com",
                            EmailConfirmed = true,
                            FirstName = "Vanya",
                            LastName = "Yankova",
                            LockoutEnabled = false,
                            NormalizedEmail = "V.YANKOVA@MAIL.COM",
                            NormalizedUserName = "V.YANKOVA@MAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEPVIbPVqWkd3C7XVlbA2beoiFDcH9dKMPLCUvnqcJEMrSbkxRAreyNKwWmKmpAQdUA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "312721bf-561a-435b-a007-bdb7fc675da4",
                            TwoFactorEnabled = false,
                            UserName = "v.yankova@mail.com"
                        });
                });

            modelBuilder.Entity("Infrastructure.Entities.Doctor", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HospitalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IdenitityId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecialtyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("HospitalId");

                    b.HasIndex("IdenitityId");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("Doctors");

                    b.HasData(
                        new
                        {
                            Id = "44a35b22-2cdb-44bd-8286-d7ec7eaa2248",
                            HospitalId = "505d9322-7cca-4bca-ae59-683ff3089872",
                            IdenitityId = "a3717562-385e-41ce-9eff-0f1b994e5548",
                            ImgUrl = "https://storage.cloud.google.com/healthsync/ivan-ivanov.jpg",
                            SpecialtyId = "d50a6c34-f6d3-4ff8-b1ad-299dcb776789"
                        },
                        new
                        {
                            Id = "43eb5263-a106-4d5b-909f-92294b21f360",
                            HospitalId = "505d9322-7cca-4bca-ae59-683ff3089872",
                            IdenitityId = "4d650e24-6b66-41e3-8391-efab8c31a1dd",
                            ImgUrl = "https://storage.cloud.google.com/healthsync/maria-marinova.jpg",
                            SpecialtyId = "c3a9d3e1-5b4b-4ae1-b96c-8ef6bdb17c0d"
                        },
                        new
                        {
                            Id = "4411897e-f897-404e-95bd-85e683b34ff5",
                            HospitalId = "505d9322-7cca-4bca-ae59-683ff3089872",
                            IdenitityId = "88cd5a7b-01d8-49b4-8688-35cd23751532",
                            ImgUrl = "https://storage.cloud.google.com/healthsync/aleks-kirilov.jpg",
                            SpecialtyId = "c3a9d3e1-5b4b-4ae1-b96c-8ef6bdb17c0d"
                        },
                        new
                        {
                            Id = "97e6c00a-664f-4861-8976-4058aed9cdb0",
                            HospitalId = "505d9322-7cca-4bca-ae59-683ff3089872",
                            IdenitityId = "95189f02-fb1a-4700-95e3-6146b8aa8b15",
                            ImgUrl = "https://storage.cloud.google.com/healthsync/kiril-conev.jpg",
                            SpecialtyId = "f24f8a2e-2f91-4be1-85a9-f1e6b4f82b74"
                        },
                        new
                        {
                            Id = "702abf92-a237-460a-94b5-5763127fb627",
                            HospitalId = "710649bb-deb0-4271-a97f-6e5cde3d2fe6",
                            IdenitityId = "f37b43ca-86a2-4b11-972d-5e0569f4deb3",
                            ImgUrl = "https://storage.cloud.google.com/healthsync/ivana-ivanova.jpg",
                            SpecialtyId = "01b2c6e4-cf32-4b44-84ae-4e4e2c17d8f9"
                        },
                        new
                        {
                            Id = "77bb4f69-7cb7-4a8d-acb2-5464b74cfff1",
                            HospitalId = "710649bb-deb0-4271-a97f-6e5cde3d2fe6",
                            IdenitityId = "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35",
                            ImgUrl = "https://storage.cloud.google.com/healthsync/monika-kirilova.jpg",
                            SpecialtyId = "f24f8a2e-2f91-4be1-85a9-f1e6b4f82b74"
                        },
                        new
                        {
                            Id = "6ba6d91c-3c17-4a90-a73b-87085a17861a",
                            HospitalId = "710649bb-deb0-4271-a97f-6e5cde3d2fe6",
                            IdenitityId = "78850da7-a0ff-42f3-a862-d162457910a0",
                            SpecialtyId = "d50a6c34-f6d3-4ff8-b1ad-299dcb776789"
                        });
                });

            modelBuilder.Entity("Infrastructure.Entities.Hospital", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hospitals");

                    b.HasData(
                        new
                        {
                            Id = "505d9322-7cca-4bca-ae59-683ff3089872",
                            Address = "456 Sunrise Avenue, Clearwater, FL 33759, USA",
                            Name = "Sunnybrook General Hospital"
                        },
                        new
                        {
                            Id = "710649bb-deb0-4271-a97f-6e5cde3d2fe6",
                            Address = "321 Maple Street, Boulder, CO 80301, USA",
                            Name = "Pine Hills Medical Center"
                        });
                });

            modelBuilder.Entity("Infrastructure.Entities.Meeting", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("DoctorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("Infrastructure.Entities.Review", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("DoctorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Reviewer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("Reviews");

                    b.HasData(
                        new
                        {
                            Id = "ba9d730a-fcf5-4662-adfd-23bfa49e10f1",
                            Date = new DateTime(2024, 10, 14, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            DoctorId = "44a35b22-2cdb-44bd-8286-d7ec7eaa2248",
                            Rating = 5,
                            Reviewer = "Aleks Petrov"
                        },
                        new
                        {
                            Id = "4d3e3800-b989-4406-85cc-f0ecd1080b88",
                            Date = new DateTime(2024, 9, 19, 10, 32, 0, 0, DateTimeKind.Unspecified),
                            DoctorId = "44a35b22-2cdb-44bd-8286-d7ec7eaa2248",
                            Rating = 2,
                            Reviewer = "Maria Kostova"
                        },
                        new
                        {
                            Id = "1f4ac07c-673a-4936-b0e4-196dd7488192",
                            Date = new DateTime(2023, 1, 24, 19, 52, 0, 0, DateTimeKind.Unspecified),
                            DoctorId = "44a35b22-2cdb-44bd-8286-d7ec7eaa2248",
                            Rating = 4,
                            Reviewer = "Kristin Angelova"
                        },
                        new
                        {
                            Id = "bd5b349e-9eba-4778-91cc-9ea18d3a41a3",
                            Date = new DateTime(2023, 1, 25, 13, 12, 0, 0, DateTimeKind.Unspecified),
                            DoctorId = "44a35b22-2cdb-44bd-8286-d7ec7eaa2248",
                            Rating = 3,
                            Reviewer = "Angel Bogdanski"
                        },
                        new
                        {
                            Id = "2c840f56-b9b4-414e-976c-c694dc4e5e80",
                            Date = new DateTime(2024, 7, 20, 18, 52, 0, 0, DateTimeKind.Unspecified),
                            DoctorId = "44a35b22-2cdb-44bd-8286-d7ec7eaa2248",
                            Rating = 6,
                            Reviewer = "Kosta Adamovich"
                        },
                        new
                        {
                            Id = "2ba604e3-80d8-40cb-8861-5d07ba7107e7",
                            Date = new DateTime(2024, 8, 4, 7, 0, 0, 0, DateTimeKind.Unspecified),
                            DoctorId = "44a35b22-2cdb-44bd-8286-d7ec7eaa2248",
                            Rating = 6,
                            Reviewer = "Kristian Ivanov"
                        },
                        new
                        {
                            Id = "d6a1dcfd-418b-4304-8744-d341beab0cf6",
                            Date = new DateTime(2024, 6, 15, 22, 12, 0, 0, DateTimeKind.Unspecified),
                            DoctorId = "44a35b22-2cdb-44bd-8286-d7ec7eaa2248",
                            Rating = 2,
                            Reviewer = "Viktor Terziev"
                        },
                        new
                        {
                            Id = "c6f58c38-5d0c-4006-8697-ac72ee11fe29",
                            Date = new DateTime(2024, 4, 2, 8, 2, 0, 0, DateTimeKind.Unspecified),
                            DoctorId = "43eb5263-a106-4d5b-909f-92294b21f360",
                            Rating = 5,
                            Reviewer = "Yordan Angelov"
                        },
                        new
                        {
                            Id = "b2089e63-a041-4638-b308-c3ab8b6551ea",
                            Date = new DateTime(2022, 12, 12, 10, 43, 0, 0, DateTimeKind.Unspecified),
                            DoctorId = "43eb5263-a106-4d5b-909f-92294b21f360",
                            Rating = 5,
                            Reviewer = "Kristian Ivanov"
                        });
                });

            modelBuilder.Entity("Infrastructure.Entities.Specialty", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Specialties");

                    b.HasData(
                        new
                        {
                            Id = "d50a6c34-f6d3-4ff8-b1ad-299dcb776789",
                            Type = "Orthodontist"
                        },
                        new
                        {
                            Id = "c3a9d3e1-5b4b-4ae1-b96c-8ef6bdb17c0d",
                            Type = "Endocrinologist"
                        },
                        new
                        {
                            Id = "f24f8a2e-2f91-4be1-85a9-f1e6b4f82b74",
                            Type = "Cardiologist"
                        },
                        new
                        {
                            Id = "01b2c6e4-cf32-4b44-84ae-4e4e2c17d8f9",
                            Type = "Neurologist"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.Doctor", b =>
                {
                    b.HasOne("Infrastructure.Entities.Hospital", "Hospital")
                        .WithMany("Doctors")
                        .HasForeignKey("HospitalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Entities.ApplicationUser", "Identity")
                        .WithMany()
                        .HasForeignKey("IdenitityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Entities.Specialty", "Specialty")
                        .WithMany("Doctors")
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hospital");

                    b.Navigation("Identity");

                    b.Navigation("Specialty");
                });

            modelBuilder.Entity("Infrastructure.Entities.Meeting", b =>
                {
                    b.HasOne("Infrastructure.Entities.Doctor", "Doctor")
                        .WithMany("Meetings")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("Infrastructure.Entities.Review", b =>
                {
                    b.HasOne("Infrastructure.Entities.Doctor", "Doctor")
                        .WithMany("Reviews")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Infrastructure.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Infrastructure.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Infrastructure.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Infrastructure.Entities.Doctor", b =>
                {
                    b.Navigation("Meetings");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Infrastructure.Entities.Hospital", b =>
                {
                    b.Navigation("Doctors");
                });

            modelBuilder.Entity("Infrastructure.Entities.Specialty", b =>
                {
                    b.Navigation("Doctors");
                });
#pragma warning restore 612, 618
        }
    }
}