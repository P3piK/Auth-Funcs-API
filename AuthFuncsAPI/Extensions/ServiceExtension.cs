using AuthFuncsAPI.Middleware;
using AuthFuncsCore.Config;
using AuthFuncsRepository.Entity;
using AuthFuncsService.Dto.Authorization;
using AuthFuncsService.Interface;
using AuthFuncsService.Service;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthFuncsAPI.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IValidator<RegisterRequestDto>, RegisterRequestDtoValidator>();
            services.AddScoped<ErrorHandlingMiddleware>();

            services.AddScoped<IAuthorizationService, AuthorizationService>();

        }

        public static void RegisterConfiguration(this IServiceCollection services, ConfigurationManager configuration)
        {
            var authenticationConfig = new AuthenticationConfig();
            configuration.GetSection("Authentication").Bind(authenticationConfig);

            services.AddSingleton(authenticationConfig);
        }

        public static void ConfigureJwtAuthentication(this IServiceCollection services, AuthenticationConfig authenticationConfig)
        {
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = authenticationConfig.JwtIssuer,
                    ValidAudience = authenticationConfig.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfig.JwtKey)),
                };
            });
        }
    }
}
