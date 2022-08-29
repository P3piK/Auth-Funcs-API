using AuthFuncsCore.Config;
using AuthFuncsRepository;
using AuthFuncsRepository.Entity;
using AuthFuncsService.Dto.Authorization;
using AuthFuncsService.Exception;
using AuthFuncsService.Interface;
using Microsoft.AspNetCore.Identity;
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
        public AuthorizationService(AFContext context, IPasswordHasher<User> passwordHasher, AuthenticationConfig authenticationConfig)
        {
            Context = context;
            PasswordHasher = passwordHasher;
            AuthenticationConfig = authenticationConfig;
        }
        #endregion

        #region Properties  
        public AFContext Context { get; }
        public IPasswordHasher<User> PasswordHasher { get; }
        public AuthenticationConfig AuthenticationConfig { get; }
        #endregion

        public LoginResponseDto Login(LoginRequestDto loginRequest)
        {
            var response = new LoginResponseDto();
            var user = Context.Users?.SingleOrDefault(u => u.Login == loginRequest.Login);

            if (user == null)
            {
                throw new BadRequestException(ExceptionMessageResource.InvalidUsernameOrPassword);
            }

            var verifyResult = PasswordHasher.VerifyHashedPassword(user, user.Password, loginRequest.Password);
            if (verifyResult == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException(ExceptionMessageResource.InvalidUsernameOrPassword);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login.ToString()),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthenticationConfig.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.Sha256);
            var expiryDate = DateTime.Now.AddDays(AuthenticationConfig.JwtExpiryDays);

            var token = new JwtSecurityToken(AuthenticationConfig.JwtIssuer, AuthenticationConfig.JwtIssuer, claims, expires: expiryDate, signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();

            response.Token = tokenHandler.WriteToken(token);

            return response;
        }

        public RegisterResponseDto RegisterUser(RegisterRequestDto registerRequest)
        {
            var ret = new RegisterResponseDto();

            var user = new User(Context)
            {
                Login = registerRequest.Login,
                RoleId = 1,         // TODO
                StatusId = 1,       // TODO
            };
            user.Password = PasswordHasher.HashPassword(user, registerRequest.Password);
            user.Persist();

            return ret;
        }
    }
}
