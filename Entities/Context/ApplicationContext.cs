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

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();
                entity.HasData(
                    new City { Id = Guid.Parse("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"), Name = "Hà Nội" },
                    new City { Id = Guid.Parse("fc446281-359c-46ec-a2b9-bf9f26014f88"), Name = "Đà Nẵng" },
                    new City { Id = Guid.Parse("6f624665-053e-45d2-8dd6-42baa124b481"), Name = "Hồ Chí Minh" }
                );
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasIndex(d => d.Name).IsUnique();
                entity.HasData(
                    //HN
                    new District 
                    { 
                        Id = Guid.Parse("bfb4be74-f5ed-4e67-94ba-7ee067a2098d"), 
                        CityId = Guid.Parse("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"), 
                        Name = "Ba Đình" 
                    },
                    new District
                    {
                        Id = Guid.Parse("b999d3eb-a753-49f4-897f-4c37002e1302"),
                        CityId = Guid.Parse("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"),
                        Name = "Cầu Giấy"
                    },
                    //DN
                    new District 
                    {
                        Id = Guid.Parse("2e8e7f13-ca6a-42c7-a1a0-fc5b4c872b3f"),
                        CityId = Guid.Parse("fc446281-359c-46ec-a2b9-bf9f26014f88"), 
                        Name = "Hải Châu" 
                    },
                    new District
                    {
                        Id = Guid.Parse("9fc84eab-708a-49a2-b819-06d0629e560a"),
                        CityId = Guid.Parse("fc446281-359c-46ec-a2b9-bf9f26014f88"),
                        Name = "Hoàng Sa"
                    },
                    //HCM
                    new District 
                    {
                        Id = Guid.Parse("53b0b29e-7b2c-4d4b-accc-f693ce746539"),
                        CityId = Guid.Parse("6f624665-053e-45d2-8dd6-42baa124b481"), 
                        Name = "Quận 1"
                    },
                    new District 
                    {
                        Id = Guid.Parse("c893d9cf-7b2d-4ebc-9c65-3f78e0fce6bb"),
                        CityId = Guid.Parse("6f624665-053e-45d2-8dd6-42baa124b481"), 
                        Name = "Quận 5" 
                    },
                    new District
                    {
                        Id = Guid.Parse("f9be5b5e-847d-47e9-847a-0150c4f608e1"),
                        CityId = Guid.Parse("6f624665-053e-45d2-8dd6-42baa124b481"),
                        Name = "Quận 10"
                    }
                );
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasData(
                    new Payment
                    {
                        Id = Guid.Parse("70d99884-b132-4913-9cc9-bf4b50885ec3"),
                        Name = "Momo",
                        Image = null
                    },
                    new Payment
                    {
                        Id = Guid.Parse("5f750901-00ba-4658-99b1-17b6173e8ce6"),
                        Name = "ZaloPay",
                        Image = null
                    },
                    new Payment
                    {
                        Id = Guid.Parse("79ed495e-52c0-4446-a347-64c913fad40f"),
                        Name = "Cash",
                        Image = null
                    });
            });
        }

    }
}
