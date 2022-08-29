using AuthFuncsRepository.Entity;
using AuthFuncsRepository.Extension;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsRepository
{
    public class AFContext : DbContext
    {
        private string connectionString =
            "Server=(localdb)\\mssqllocaldb;Database=AuthFuncsDb;Trusted_Connection=True";

        public DbSet<User> Users { get; set; }
        public DbSet<UserStatus> UserStatuses { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.BuildUserEntity();

            // seed
            modelBuilder.SeedUserStatus();
            modelBuilder.SeedUserRoles();
            modelBuilder.SeedUsers();
        }
    }
}
