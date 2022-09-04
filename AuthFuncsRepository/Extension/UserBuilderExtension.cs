using AuthFuncsRepository.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsRepository.Extension
{
    public static class UserBuilderExtension
    {
        public static void BuildUserEntity (this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Login).IsRequired().HasMaxLength(25);
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
        }

        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new List<User>() 
            {
                new User() 
                { 
                    Id = 1, 
                    Login = "admin", 
                    Password = "AQAAAAEAACcQAAAAEH7ClDMgufncQOTcKuYJUkXMrMFSegWBHFAQphveG7ph/NI3O86jRBbRnwtsqCGLNA==", 
                    StatusId = 1, 
                    RoleId = 1, 
                    Modified = new DateTime(2022,9,2) 
                },                
                new User()
                {
                    Id = 2,
                    Login = "UserTest",
                    Password = "AQAAAAEAACcQAAAAEH7ClDMgufncQOTcKuYJUkXMrMFSegWBHFAQphveG7ph/NI3O86jRBbRnwtsqCGLNA==",
                    StatusId = 1,
                    RoleId = 1,
                    Modified = new DateTime(2022,9,4)
                },
                new User()
                {
                    Id = 3,
                    Login = "UserSuperuser",
                    Password = "AQAAAAEAACcQAAAAEH7ClDMgufncQOTcKuYJUkXMrMFSegWBHFAQphveG7ph/NI3O86jRBbRnwtsqCGLNA==",
                    StatusId = 1,
                    RoleId = 2,
                    Modified = new DateTime(2022,9,4)
                },
                new User()
                {
                    Id = 4,
                    Login = "UserAdmin",
                    Password = "AQAAAAEAACcQAAAAEH7ClDMgufncQOTcKuYJUkXMrMFSegWBHFAQphveG7ph/NI3O86jRBbRnwtsqCGLNA==",
                    StatusId = 1,
                    RoleId = 3,
                    Modified = new DateTime(2022,9,4)
                }
            });
        }
    }
}
