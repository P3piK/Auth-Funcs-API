using System.ComponentModel.DataAnnotations;

namespace AuthFuncsService.Dto.Authorization
{
    public class RegisterRequestDto
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
