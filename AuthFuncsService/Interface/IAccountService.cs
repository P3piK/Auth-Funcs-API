using AuthFuncsService.Dto.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsService.Interface
{
    public interface IAccountService
    {
        IEnumerable<AccountDto> FindAll();
        AccountDto FindById(int id);
        void Update(int id, AccountDto account);
        void Deactivate(int id);

    }
}
