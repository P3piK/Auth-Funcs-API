using AuthFuncsRepository.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsService.Dto.Account
{
    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<User, AccountDto>();
        }
    }
}
