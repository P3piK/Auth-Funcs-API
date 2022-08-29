using AuthFuncsRepository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsService.Dto.Authorization
{
    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestDtoValidator(AFContext dbContext)
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(25);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);

            RuleFor(x => x.Login)
                .Custom((value, context) =>
                {
                    if (dbContext.Users.Any(u => u.Login == value))
                    {
                        context.AddFailure("login", "Login already in use");
                    }
                });
        }
    }
}
