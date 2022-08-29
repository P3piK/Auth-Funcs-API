using System.ComponentModel.DataAnnotations;

namespace AuthFuncsService.Dto.Authorization
{
    public class LoginRequestDto
    {
        public string Login { get; set; }
        public string Password { get; set; }

    }
}
