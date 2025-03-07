using Core.Interfaces.ExternalServices;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Services;
using Core.Services.Configs;
using Infrastructure;
using Infrastructure.Database.Entities;
using Infrastructure.Database.Repositories;
using Infrastructure.Services;
using Infrastructure.Services.Configs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestAPI.Filters;
using RestAPI.Services;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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
                    ValidIssuer = config["JwtToken:Issuer"],
                    ValidAudience = config["JwtToken:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtToken:Key"])),
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
                    },
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/chat")))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }

        public static void AddCorsExtension(this IServiceCollection services, IConfiguration config)
        {
            var allowedOrigins = config.GetSection("AllowedOrigins").Get<string[]>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins(allowedOrigins)
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
            services.AddTransient<IEmailSenderService, EmailSenderService>();
            services.AddTransient<IMemoryCacheService, MemoryCacheService>();
            services.AddSingleton<IJwtTokenService, JwtTokenService>();
            services.AddSingleton<IGoogleCloudStorageService, GoogleCloudStorageService>();
            services.AddScoped<IDoctorsService, DoctorsService>();
            services.AddScoped<IMeetingsService, MeetingsService>();
            services.AddScoped<IReviewsService, ReviewsService>();
            services.AddScoped<IDoctorScheduleService, DoctorScheduleService>();
            services.AddScoped<IHospitalsService, HospitalsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IChatService, ChatService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDoctorsRepository, DoctorsRepository>();
            services.AddScoped<IDoctorScheduleRepository, DoctorScheduleRepository>();
            services.AddScoped<IHospitalsRepository, HospitalsRepository>();
            services.AddScoped<IMeetingsRepository, MeetingsRepository>();
            services.AddScoped<IReviewsRepository, ReviewsRepository>();
            services.AddScoped<ISpecialtiesRepository, SpecialtiesRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
        }

        public static void AddConfigs(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JsonSerializerOptions>(options => new JsonSerializerOptions(JsonSerializerDefaults.Web));
            services.Configure<EmailSenderConfig>(config.GetSection("EmailSettings"));
            services.Configure<JwtTokenConfig>(config.GetSection("JwtToken"));
            services.Configure<CookiesConfig>(config.GetSection("Cookies"));
        }
    }
}
