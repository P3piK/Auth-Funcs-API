using AuthFuncsRepository.Entity;
using AuthFuncsRepository.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsRepository
{
    public class AFContext : DbContext
    {
        private readonly string connectionString;

        public AFContext(IConfiguration configuration)
        {
            connectionString = configuration["DbConnectionString"];
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserStatus> UserStatuses { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(true);
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.BuildUserStatusEntity();
            modelBuilder.BuildUserRoleEntity();
            modelBuilder.BuildUserEntity();

            // seed
            modelBuilder.SeedUserStatus();
            modelBuilder.SeedUserRoles();
            modelBuilder.SeedUsers();
        }
    }
}
