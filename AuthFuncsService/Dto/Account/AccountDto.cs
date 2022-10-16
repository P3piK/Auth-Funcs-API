using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsService.Dto.Account
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
    }
}
