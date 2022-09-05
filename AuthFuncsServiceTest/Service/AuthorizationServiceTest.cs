using Microsoft.VisualStudio.TestTools.UnitTesting;
using AuthFuncsService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthFuncsService.Interface;
using AuthFuncsService.Dto.Authorization;
using Microsoft.Extensions.DependencyInjection;
using AuthFuncsRepository;
using Microsoft.AspNetCore.Identity;
using AuthFuncsRepository.Entity;
using AuthFuncsCore.Config;

namespace AuthFuncsService.Service.Test
{
    [TestClass()]
    public class AuthorizationServiceTest
    {
        public IAuthorizationService AuthorizationService { get; }

        public AuthorizationServiceTest()
        {
        }

        //[TestMethod()]
        public void RegisterUserTest()
        {
            var registerRequest = new RegisterRequestDto()
            {
                Login = Guid.NewGuid().ToString(),
                Password = "Test1234",
                ConfirmPassword = "Test1234",
            };
            var ret = AuthorizationService.RegisterUser(registerRequest);
            
            Assert.IsNotNull(ret.Id);
            Assert.AreEqual(registerRequest.Login, ret.Login);
            Assert.AreEqual("User", ret.Role);
            Assert.AreEqual("Active", ret.Status);
        }

        //[TestMethod()]
        public void LoginTest()
        {

        }
    }
}