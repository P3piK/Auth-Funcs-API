using AuthFuncsCore.Config;
using AuthFuncsRepository;
using AuthFuncsRepository.Entity;
using AuthFuncsService.Dto.Authorization;
using AuthFuncsService.Exception;
using AuthFuncsService.Interface;
using AuthFuncsWorkerService;
using AuthFuncsWorkerService.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsService.Service
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Constructor

        public AuthorizationService(AFContext context, 
            IUserPrincipalService userPrincipalService,
            IPasswordHasher<User> passwordHasher, 
            AuthenticationConfig authenticationConfig,
            INotificationService notificationService)
        {
            Context = context;
            UserPrincipalService = userPrincipalService;
            PasswordHasher = passwordHasher;
            AuthenticationConfig = authenticationConfig;
            NotificationService = notificationService;
        }

        #endregion

        #region Properties  
        public AFContext Context { get; }
        public IUserPrincipalService UserPrincipalService { get; }
        public IPasswordHasher<User> PasswordHasher { get; }
        public AuthenticationConfig AuthenticationConfig { get; }
        public INotificationService NotificationService { get; }
        #endregion

        public LoginResponseDto Login(LoginRequestDto loginRequest)
        {
            var response = new LoginResponseDto();
            var login = PurifyTextField(loginRequest.Login);
            var user = Context.Users?
                .SingleOrDefault(u => u.Login == loginRequest.Login);

            if (user == null)
            {
                throw new BadRequestException(ExceptionMessageResource.InvalidUsernameOrPassword);
            }

            var verifyResult = PasswordHasher.VerifyHashedPassword(user, user.Password, loginRequest.Password);
            if (verifyResult == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException(ExceptionMessageResource.InvalidUsernameOrPassword);
            }

            if (user.Status != UserStatusEnum.Active)
            {
                throw new BadRequestException($"Invalid user status: {user.Status.Name}");
            }


            response.Token = WriteJwtToken(user);

            return response;
        }

        public RegisterResponseDto RegisterUser(RegisterRequestDto registerRequest)
        {
            var user = new User(Context)
            {
                Login = PurifyTextField(registerRequest.Login),
                Role = UserRoleEnum.User,
                Status = UserStatusEnum.Active,
                ModifierId = UserPrincipalService.GetUserId,
            };
            user.Password = PasswordHasher.HashPassword(user, registerRequest.Password);
            user.Persist();

            return new RegisterResponseDto()
            {
                Id = user.Id,
                Login = user.Login,
                Role = user.Role.Name,
                Status = user.Status.Name,
            };
        }

        public void ResetPassword(string login)
        {
            var user = Context.Users.FirstOrDefault(u => u.Login == login);
            if (user != null)
            {
                NotificationService.SendNotificationAsync(login, EmailWorkerActionName.PasswordReset);

                user.Status = UserStatusEnum.PasswordReset;
                user.Persist();
            }
        }

        #region Private

        private string WriteJwtToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login.ToString()),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthenticationConfig.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiryDate = DateTime.Now.AddDays(AuthenticationConfig.JwtExpiryDays);

            var token = new JwtSecurityToken(AuthenticationConfig.JwtIssuer, AuthenticationConfig.JwtIssuer, claims, expires: expiryDate, signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        private static string PurifyTextField(string? login)
        {
            if (String.IsNullOrEmpty(login))
            {
                return login;
            }

            return login.Trim().ToLower();
        }

        #endregion
    }
}
