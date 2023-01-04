using AuthFuncsRepository.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsService.Dto.Account
{
    public class AccountDtoValidator : AbstractValidator<AccountDto>
    {
        public AccountDtoValidator()
        {
            RuleFor(x => x.Login)
                .EmailAddress();

            RuleFor(x => x.Role)
                .Custom((value, context) =>
                {
                    if (!Enum.TryParse(value, out UserRoleEnum _))
                    {
                        context.AddFailure("Role not supported.");
                    }
                });

            RuleFor(x => x.Status)
                .Custom((value, context) =>
                {
                    if (!Enum.TryParse(value, out UserStatusEnum _))
                    {
                        context.AddFailure("Status not supported.");
                    }
                });
        }
    }
}
