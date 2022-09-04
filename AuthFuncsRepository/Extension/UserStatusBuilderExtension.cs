using AuthFuncsRepository.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsRepository.Extension
{
    public static class UserStatusBuilderExtension
    {
        public static void BuildUserStatusEntity(this ModelBuilder modelBuilder)
        {
        }

        public static void SeedUserStatus(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserStatus>().HasData(new List<UserStatus>()
            {
                new UserStatus() { Id = 1, Name = "Active", Created = new DateTime(2022,9,2) },
                new UserStatus() { Id = 2, Name = "Inactive", Created = new DateTime(2022,9,2) },
                new UserStatus() { Id = 3, Name = "PasswordReset", Created = new DateTime(2022,9,2) },
                new UserStatus() { Id = 4, Name = "NotConfirmed", Created = new DateTime(2022,9,2) },
            });
        }
    }
}
