using Microsoft.EntityFrameworkCore;
using Store.Application.Interface.Context;
using Store.Common.Role;
using Store.Domain.Entities;

namespace Store.Presistance.Context
{
    public class DataBaseContext : DbContext,IDataBaseContext
    {
        public DataBaseContext(DbContextOptions Options) : base(Options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = nameof(UserRole.Admin) });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 2, Name = nameof(UserRole.Operator) });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 3, Name = nameof(UserRole.Customer) });

            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();

            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsRemoved);
        }

    }
}
