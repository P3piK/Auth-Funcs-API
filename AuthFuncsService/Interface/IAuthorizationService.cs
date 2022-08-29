using AuthFuncsService.Dto.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsService.Interface
{
    public interface IAuthorizationService
    {
        RegisterResponseDto RegisterUser(RegisterRequestDto registerRequest);
        LoginResponseDto Login(LoginRequestDto loginRequest);

    }
}
