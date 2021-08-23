using System;
using System.Text;
using Domain.Interfaces;
using Domain.Models;
using Infrastucture;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Service.JwtTokens;
using Service.MappingProfiles;
using Service.PasswordHashers;
using Service.Services;
using Service.Services.Impl;

namespace api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddJwtTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITokensGenerator, JwtTokensGenerator>();
            services.AddSingleton<IRefreshTokenValidator, JwtRefreshTokenValidator>();
            services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();

            var authenticationConfiguration = new AuthenticationConfiguration();
            configuration.Bind("Authentication", authenticationConfiguration);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(authenticationConfiguration.AccessTokenSecret)),
                        ValidIssuer = authenticationConfiguration.Issuer,
                        ValidAudience = authenticationConfiguration.Audience,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICinemasService, CinemaService>();
            services.AddScoped<IAuthService, AuthService>();
        }

        public static void AddCorsPolicies(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }

        public static void AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApiMappingProfile),
                typeof(CinemaProfile),
                typeof(MapProfile));
        }
    }
}