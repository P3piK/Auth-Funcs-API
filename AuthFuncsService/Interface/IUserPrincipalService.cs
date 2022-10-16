using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsService.Interface
{
    public interface IUserPrincipalService
    {
        ClaimsPrincipal? User { get; }
        int? GetUserId { get; }
    }
}
