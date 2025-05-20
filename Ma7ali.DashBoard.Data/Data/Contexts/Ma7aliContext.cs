using Ma7ali.DashBoard.Data.Entities.CartEntities;
using Ma7ali.DashBoard.Data.Entities.OrderEntities;
using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using Ma7ali.DashBoard.Data.Entities.StoreEntities;
using Ma7ali.DashBoard.Data.Entities.UserEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Data.Data.Contexts
{
    public class Ma7aliContext : IdentityDbContext<User>
    {
        public Ma7aliContext(DbContextOptions<Ma7aliContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Ma7aliContext).Assembly);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveyMethod> DeliveyMethods { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ProductImage> Images { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
