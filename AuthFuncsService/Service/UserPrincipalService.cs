using AuthFuncsService.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsService.Service
{
    public class UserPrincipalService : IUserPrincipalService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserPrincipalService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

        public int? GetUserId
        {
            get
            {
                var claim = User.FindFirstValue(nameof(ClaimTypes.NameIdentifier));
                if (!String.IsNullOrEmpty(claim))
                {
                    return Int32.Parse(claim);
                }
                return null;
            }
        }
    }
}
