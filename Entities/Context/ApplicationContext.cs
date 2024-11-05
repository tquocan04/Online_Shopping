using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Online_Shopping.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Buy_Product> Buy_Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Cus_Address> CusAddresses { get; set; }
        public DbSet<Cus_Voucher> CusVouchers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Import_Bill> ImportBills { get; set; }
        public DbSet<Import_Product> ImportProducts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(p => p.Name).IsUnique();
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasIndex(s => s.Name).IsUnique();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Username).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(c => new { c.ProductId, c.CustomerId });
            });

            modelBuilder.Entity<Buy_Product>(entity =>
            {
                entity.HasKey(bp => new { bp.ProductId, bp.CustomerId, bp.BillId });
            });

            modelBuilder.Entity<Import_Product>(entity =>
            {
                entity.HasKey(ip => new { ip.ProductId, ip.ImportBillId, ip.SupplierId });
            });

            modelBuilder.Entity<Cus_Address>(entity =>
            {
                entity.HasKey(ca => new { ca.DistrictId, ca.Street, ca.UserId });
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.UserId });
            });
            
            modelBuilder.Entity<Cus_Voucher>(entity =>
            {
                entity.HasKey(cv => new { cv.VoucherId, cv.CustomerId });
            });
        }

    }
}
