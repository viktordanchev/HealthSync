﻿using RestAPI.Services;
using RestAPI.Services.Contracts;
using Infrastructure;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Core.Services.Contracts;
using Core.Services;

namespace Server.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<HealthSyncDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<HealthSyncDbContext>()
            .AddDefaultTokenProviders();
        }

        public static void AddAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var userId = context.Principal.FindFirst("Identifier").Value;
                        var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                        var user = await userManager.FindByIdAsync(userId);

                        if (user == null)
                        {
                            context.Fail("Unauthorized user");
                        }
                    }
                };
            });
        }

        public static void AddCorsExtension(this IServiceCollection services, IConfiguration config)
        {
            var origin = config.GetValue<string>("AccessControlAllowOrigin");

            services.AddCors(options =>
            {
                options.AddPolicy("Cors", builder =>
                {
                    builder.WithOrigins(origin)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IJWTTokenService, JWTTokenService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IMemoryCacheService, MemoryCacheService>();
            services.AddScoped<IDoctorService, DoctorService>();
        }
    }
}
