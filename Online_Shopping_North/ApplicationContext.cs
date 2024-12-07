using Microsoft.EntityFrameworkCore;
using Online_Shopping_North.Entities;

namespace Online_Shopping_North
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BranchProduct> BranchProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(c => c.Email).IsUnique();

                entity.HasMany(c => c.Addresses)
                    .WithOne(a => a.Customer)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();

                entity.HasMany(b => b.Employees)
                    .WithOne(e => e.Branch)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(b => b.Addresses)
                    .WithOne(a => a.Branch)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(p => p.Name).IsUnique();
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(i => new { i.ProductId, i.OrderId });
            });

            modelBuilder.Entity<BranchProduct>(entity =>
            {
                entity.HasKey(bp => new { bp.ProductId, bp.BranchId });
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(c => c.Email).IsUnique();
                entity.HasIndex(c => c.Username).IsUnique();

                entity.HasMany(e => e.Addresses)
                    .WithOne(a => a.Employee)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.HasIndex(v => v.Code).IsUnique();

                entity.HasMany(v => v.Orders)
                    .WithOne(o => o.Voucher)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer) // Thiết lập quan hệ 1-n
                .WithMany(c => c.Orders) // Customer có nhiều Orders
                .OnDelete(DeleteBehavior.Cascade); // Xóa cascade

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();
                entity.HasData(
                    new Region { Id = "Bac", Name = "Miền Bắc" },
                    new Region { Id = "Trung", Name = "Miền Trung" },
                    new Region { Id = "Nam", Name = "Miền Nam" }
                );
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();
                entity.HasData(
                    new City
                    {
                        Id = Guid.Parse("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"),
                        Name = "Hà Nội",
                        RegionId = "Bac"
                    },
                    new City
                    {
                        Id = Guid.Parse("fc446281-359c-46ec-a2b9-bf9f26014f88"),
                        Name = "Đà Nẵng",
                        RegionId = "Trung"
                    },
                    new City
                    {
                        Id = Guid.Parse("6f624665-053e-45d2-8dd6-42baa124b481"),
                        Name = "Hồ Chí Minh",
                        RegionId = "Nam"
                    }
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
                        Id = "MOMO",
                        Name = "MOMO",
                        Image = null
                    },
                    new Payment
                    {
                        Id = "ZALOPAY",
                        Name = "ZALOPAY",
                        Image = null
                    },
                    new Payment
                    {
                        Id = "CASH",
                        Name = "CASH",
                        Image = null
                    });
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasData(
                    new Role
                    {
                        Id = "Staff",
                        Name = "STAFF",
                    },
                    new Role
                    {
                        Id = "Admin",
                        Name = "ADMIN",
                    });
            });

            modelBuilder.Entity<ShippingMethod>(entity =>
            {
                entity.HasData(
                    new ShippingMethod
                    {
                        Id = "GRAB",
                        Name = "GRAB",
                    },
                    new ShippingMethod
                    {
                        Id = "BE",
                        Name = "BE",
                    },
                    new ShippingMethod
                    {
                        Id = "SHOPEEFOOD",
                        Name = "SHOPEE FOOD",
                    });
            });
        }
    }
}
