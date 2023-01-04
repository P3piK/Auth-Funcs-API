using AuthFuncsRepository;
using AuthFuncsRepository.Entity;
using AuthFuncsService.Dto.Account;
using AuthFuncsService.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsService.Service
{
    public class AccountService : IAccountService
    {
        public AccountService(AFContext context, 
            IUserPrincipalService userPrincipalService,
            IMapper mapper)
        {
            Context = context;
            UserPrincipalService = userPrincipalService;
            Mapper = mapper;
        }

        public AFContext Context { get; }
        public IUserPrincipalService UserPrincipalService { get; }
        public IMapper Mapper { get; }

        public void Deactivate(int id)
        {
            var user = Context.Users.Find(id);

            if (user != null)
            {
                user.Status = UserStatusEnum.Inactive;
                user.ModifierId = UserPrincipalService.GetUserId;
                user.Persist();
            }
        }

        public IEnumerable<AccountDto> FindAll()
        {
            return Context.Users
                .Select(u => Mapper.Map<AccountDto>(u))
                .ToList();
        }

        public AccountDto FindById(int id)
        {
            var ret = new AccountDto();
            var account = Context.Users.Find(id);
            
            if (account != null)
            {
                ret = Mapper.Map<AccountDto>(account);
            }

            return ret;
        }

        public void Update(int id, AccountDto account)
        {
            var user = Context.Users.Find(id);
            
            if (user != null)
            {
                user.Login = account.Login;
                user.Status = Enum.Parse<UserStatusEnum>(account.Status);
                user.Role = Enum.Parse<UserRoleEnum>(account.Role);

                user.ModifierId = UserPrincipalService.GetUserId;
                user.Persist();
            }
        }
    }
}
