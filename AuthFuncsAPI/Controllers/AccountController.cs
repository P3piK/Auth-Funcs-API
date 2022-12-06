using AuthFuncsService.Dto.Account;
using AuthFuncsService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthFuncsAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        #region Properties

        public IAccountService AccountService { get; }

        #endregion

        #region Constructor

        public AccountController(IAccountService accountService)
        {
            AccountService = accountService;
        }

        #endregion

        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<AccountDto> Get()
        {
            return AccountService.FindAll();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public AccountDto Get(int id)
        {
            return AccountService.FindById(id);
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] AccountDto value)
        {
            AccountService.Update(id, value);
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "")]
        public void Deactivate(int id)
        {
            AccountService.Deactivate(id);
        }
    }
}
