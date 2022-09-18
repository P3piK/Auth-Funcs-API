using AuthFuncsCore.Config;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthFuncsAPI.Extensions
{
    public static class ConfigurationExtension
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
