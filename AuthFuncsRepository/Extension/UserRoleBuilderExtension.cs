using AuthFuncsRepository.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsRepository.Extension
{
    public static class UserRoleBuilderExtension
    {
        public static void BuildUserRoleEntity(this ModelBuilder modelBuilder)
        {
        }

        public static void SeedUserRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasData(new List<UserRole>()
            {
                new UserRole() { Id = 1, Name = "User", Created = new DateTime(2022,9,2) },
                new UserRole() { Id = 2, Name = "Superuser", Created = new DateTime(2022,9,2) },
                new UserRole() { Id = 3, Name = "Admin", Created = new DateTime(2022,9,2) },
            });
        }
    }
}
