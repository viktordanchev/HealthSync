using RestAPI.Services;
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
using RestAPI.Filters;
using System.Text.Json.Serialization;
using System.Security.Claims;

namespace Server.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddDbContextExtension(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<HealthSyncDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        public static void AddIdentityExtension(this IServiceCollection services)
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

        public static void AddJWTAuthentication(this IServiceCollection services, IConfiguration config)
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var userId = context.Principal.FindFirst(ClaimTypes.NameIdentifier).Value;
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

        public static void AddControllersExtension(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ModelStateValidationFilter>();
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IJWTTokenService, JWTTokenService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IMemoryCacheService, MemoryCacheService>();
            services.AddScoped<IDoctorsService, DoctorsService>();
            services.AddScoped<IMeetingsService, MeetingsService>();
            services.AddScoped<IReviewsService, ReviewsService>();
            services.AddScoped<IDoctorScheduleService, DoctorScheduleService>();
        }
    }
}
