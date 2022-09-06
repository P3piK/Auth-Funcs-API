using AuthFuncsService.Dto.Authorization;
using AuthFuncsService.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthFuncsService.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        public AuthorizationController(IAuthorizationService authorizationService)
        {
            AuthorizationService = authorizationService;
        }

        public IAuthorizationService AuthorizationService { get; }

        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginRequestDto loginRequest)
        {
            var ret = AuthorizationService.Login(loginRequest);

            return Ok(ret);
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody]RegisterRequestDto registerRequest)
        {
            var ret = AuthorizationService.RegisterUser(registerRequest);

            return Created("user", ret);
        }

        [HttpPost("forgotpassword")]
        public ActionResult ForgotPassword([FromBody]ForgotPasswordDto request)
        {
            AuthorizationService.ForgotPassword(request.Login);
            return Ok();
        }
    }
}
