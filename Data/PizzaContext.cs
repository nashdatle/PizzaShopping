using Microsoft.EntityFrameworkCore;
using PizzaShopping.Models;

namespace PizzaShopping.Data
{
    public class PizzaContext : DbContext
    {
        public PizzaContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(en =>
            {
                en.HasKey(e => e.AccountId);
            });

            modelBuilder.Entity<Category>(en =>
            {
                en.HasKey(e => e.CategoryId);
            });

            modelBuilder.Entity<Supplier>(en =>
            {
                en.HasKey(e => e.SupplierId);
            });

            modelBuilder.Entity<Customer>(en =>
            {
                en.HasKey(e => e.CustomerId);
            });

            modelBuilder.Entity<Product>(en =>
            {
                en.HasKey(e => e.ProductId);

                en.HasOne(s => s.Supplier).WithMany(p => p.Products).HasForeignKey(p => p.SupplierId);

                en.HasOne(s => s.Category).WithMany(p => p.Products).HasForeignKey(p => p.CategoryId);
            });

            modelBuilder.Entity<Order>(en =>
            {
                en.HasKey(p => p.OrderId);

                en.HasOne(c => c.Customer).WithMany(o => o.Orders).HasForeignKey(o => o.CustomerId);

            });

            modelBuilder.Entity<OrderDetail>(en =>
            {
                en.HasNoKey();

                en.HasOne(c => c.Order).WithMany().HasForeignKey(o => o.OrderId);

                en.HasOne(o => o.Product).WithMany().HasForeignKey(od => od.ProductId);

            });

        }
    }
}
