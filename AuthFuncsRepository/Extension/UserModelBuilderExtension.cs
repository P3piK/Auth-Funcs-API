using AuthFuncsRepository.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsRepository.Extension
{
    public static class UserModelBuilderExtension
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
                new User() { Id = 1, Login = "admin", Password = "AQAAAAEAACcQAAAAEH7ClDMgufncQOTcKuYJUkXMrMFSegWBHFAQphveG7ph/NI3O86jRBbRnwtsqCGLNA==", StatusId = 1, RoleId = 1, Modified = DateTime.Now }
            });
        }

        public static void SeedUserStatus(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserStatus>().HasData(new List<UserStatus>()
            {
                new UserStatus() { Id = 1, Name = "Active", Created = DateTime.Now },
                new UserStatus() { Id = 2, Name = "Inactive", Created = DateTime.Now },
                new UserStatus() { Id = 3, Name = "PasswordReset", Created = DateTime.Now },
                new UserStatus() { Id = 4, Name = "NotConfirmed", Created = DateTime.Now },
            });
        }

        public static void SeedUserRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasData(new List<UserRole>()
            {
                new UserRole() { Id = 1, Name = "User", Created = DateTime.Now },
                new UserRole() { Id = 2, Name = "Superuser", Created = DateTime.Now },
                new UserRole() { Id = 3, Name = "Admin", Created = DateTime.Now },
            });
        }
    }
}
