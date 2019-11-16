using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForSatpaev.modules.item.entity;
using TestForSatpaev.modules.order.entity;
using TestForSatpaev.modules.region.entity;
using TestForSatpaev.modules.user.entity;

namespace TestForSatpaev
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(x => new { x.RoleId, x.UserId });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-R6DTGEL\\SQLEXPRESS;Database=SatpaevTest;Trusted_Connection=True;");
        }

        public Context()
        {

        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        //EntityFrameworkCore\Add-Migration DELET
    }
}
