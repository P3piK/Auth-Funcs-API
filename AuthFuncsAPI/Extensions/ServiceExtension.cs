using AuthFuncsAPI.Middleware;
using AuthFuncsCore.Config;
using AuthFuncsRepository.Entity;
using AuthFuncsService.Dto.Account;
using AuthFuncsService.Dto.Authorization;
using AuthFuncsService.Interface;
using AuthFuncsService.Service;
using AuthFuncsWorkerService;
using AuthFuncsWorkerService.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Text;

namespace AuthFuncsAPI.Extensions
{
    public static class ServiceExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IUserPrincipalService, UserPrincipalService>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddSingleton<INotificationService, EmailWorker>();
        }

        public static void RegisterMiddleware(this IServiceCollection services)
        {
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<RequestTimerMiddleware>();
        }

        public static void RegisterMiscs(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IValidator<RegisterRequestDto>, RegisterRequestDtoValidator>();
            services.AddScoped<IValidator<AccountDto>, AccountDtoValidator>();
            services.AddTransient<Stopwatch>();
        }
    }
}
