using Microsoft.EntityFrameworkCore;
using Store.Application.Interface.Context;
using Store.Common.Role;
using Store.Domain.Entities.Pictures;
using Store.Domain.Entities.Product;
using Store.Domain.Entities.Users;

namespace Store.Presistance.Context
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions Options) : base(Options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<LoginBackGround> LoginBackGrounds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seed Data
            SeedData(modelBuilder);

            //اعمال ایندکس بر روی ایمیل
            //اعمال عدم تکراری بودن ایمیل
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();

            //عدم نمایش فایل های حذف شده
            ApplyQueryFilter(modelBuilder);

        }

        private void ApplyQueryFilter(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsRemoved);
            modelBuilder.Entity<Category>().HasQueryFilter(x => !x.IsRemoved);
            modelBuilder.Entity<Role>().HasQueryFilter(x => !x.IsRemoved);
            modelBuilder.Entity<UserInRole>().HasQueryFilter(x => !x.IsRemoved);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = nameof(UserRole.Admin) });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 2, Name = nameof(UserRole.Operator) });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 3, Name = nameof(UserRole.Customer) });
        }

    }
}
