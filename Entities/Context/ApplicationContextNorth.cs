using Entities.Entities;
using Entities.Entities.North;
using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;

namespace Entities.Context
{
    public class ApplicationContextNorth : DbContext
    {
        public ApplicationContextNorth(DbContextOptions<ApplicationContextNorth> options) : base(options) { }

        public DbSet<AddressNorth> Addresses { get; set; }
        public DbSet<BranchNorth> Branches { get; set; }
        public DbSet<Branch_ProductNorth> BranchProducts { get; set; }
        public DbSet<CategoryNorth> Categories { get; set; }
        public DbSet<CredentialNorth> Credentials { get; set; }
        public DbSet<CityNorth> Cities { get; set; }
        public DbSet<CustomerNorth> Customers { get; set; }
        public DbSet<DistrictNorth> Districts { get; set; }
        public DbSet<EmployeeNorth> Employees { get; set; }
        public DbSet<ItemNorth> Items { get; set; }
        public DbSet<OrderNorth> Orders { get; set; }
        public DbSet<PaymentNorth> Payments { get; set; }
        public DbSet<ProductNorth> Products { get; set; }
        public DbSet<RegionNorth> Regions { get; set; }
        public DbSet<RoleNorth> Roles { get; set; }
        public DbSet<ShippingMethodNorth> ShippingMethods { get; set; }
        public DbSet<VoucherNorth> Vouchers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryNorth>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();
            });

            modelBuilder.Entity<CustomerNorth>(entity =>
            {
                entity.HasIndex(c => c.Email).IsUnique();
            });

            modelBuilder.Entity<BranchNorth>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();

            });

            modelBuilder.Entity<ProductNorth>(entity =>
            {
                entity.HasIndex(p => p.Name).IsUnique();
            });

            modelBuilder.Entity<ItemNorth>(entity =>
            {
                entity.HasKey(i => new { i.ProductId, i.OrderId });
            });

            modelBuilder.Entity<Branch_ProductNorth>(entity =>
            {
                entity.HasKey(bp => new { bp.ProductId, bp.BranchId });
            });

            modelBuilder.Entity<EmployeeNorth>(entity =>
            {
                entity.HasIndex(c => c.Email).IsUnique();
                entity.HasIndex(c => c.Username).IsUnique();
            });

            modelBuilder.Entity<OrderNorth>()
                .HasOne(o => o.Customer) // Thiết lập quan hệ 1-n
                .WithMany(c => c.Orders) // Customer có nhiều Orders
                .OnDelete(DeleteBehavior.Cascade); // Xóa cascade

            modelBuilder.Entity<RegionNorth>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();
                entity.HasData(
                    new RegionNorth { Id = "Bac", Name = "Miền Bắc" },
                    new RegionNorth { Id = "Trung", Name = "Miền Trung" },
                    new RegionNorth { Id = "Nam", Name = "Miền Nam" }
                );
            });

            modelBuilder.Entity<CityNorth>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();
                entity.HasData(
                    new CityNorth
                    {
                        Id = Guid.Parse("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"),
                        Name = "Hà Nội",
                        RegionId = "Bac"
                    },
                    new CityNorth
                    {
                        Id = Guid.Parse("fc446281-359c-46ec-a2b9-bf9f26014f88"),
                        Name = "Đà Nẵng",
                        RegionId = "Trung"
                    },
                    new CityNorth
                    {
                        Id = Guid.Parse("6f624665-053e-45d2-8dd6-42baa124b481"),
                        Name = "Hồ Chí Minh",
                        RegionId = "Nam"
                    }
                );
            });

            modelBuilder.Entity<DistrictNorth>(entity =>
            {
                entity.HasIndex(d => d.Name).IsUnique();
                entity.HasData(
                    //HN
                    new DistrictNorth
                    {
                        Id = Guid.Parse("bfb4be74-f5ed-4e67-94ba-7ee067a2098d"),
                        CityId = Guid.Parse("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"),
                        Name = "Ba Đình"
                    },
                    new DistrictNorth
                    {
                        Id = Guid.Parse("b999d3eb-a753-49f4-897f-4c37002e1302"),
                        CityId = Guid.Parse("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"),
                        Name = "Cầu Giấy"
                    },
                    //DN
                    new DistrictNorth
                    {
                        Id = Guid.Parse("2e8e7f13-ca6a-42c7-a1a0-fc5b4c872b3f"),
                        CityId = Guid.Parse("fc446281-359c-46ec-a2b9-bf9f26014f88"),
                        Name = "Hải Châu"
                    },
                    new DistrictNorth
                    {
                        Id = Guid.Parse("9fc84eab-708a-49a2-b819-06d0629e560a"),
                        CityId = Guid.Parse("fc446281-359c-46ec-a2b9-bf9f26014f88"),
                        Name = "Hoàng Sa"
                    },
                    //HCM
                    new DistrictNorth
                    {
                        Id = Guid.Parse("53b0b29e-7b2c-4d4b-accc-f693ce746539"),
                        CityId = Guid.Parse("6f624665-053e-45d2-8dd6-42baa124b481"),
                        Name = "Quận 1"
                    },
                    new DistrictNorth
                    {
                        Id = Guid.Parse("c893d9cf-7b2d-4ebc-9c65-3f78e0fce6bb"),
                        CityId = Guid.Parse("6f624665-053e-45d2-8dd6-42baa124b481"),
                        Name = "Quận 5"
                    },
                    new DistrictNorth
                    {
                        Id = Guid.Parse("f9be5b5e-847d-47e9-847a-0150c4f608e1"),
                        CityId = Guid.Parse("6f624665-053e-45d2-8dd6-42baa124b481"),
                        Name = "Quận 10"
                    }
                );
            });

            modelBuilder.Entity<PaymentNorth>(entity =>
            {
                entity.HasData(
                    new PaymentNorth
                    {
                        Id = "MOMO",
                        Name = "MOMO",
                        Image = null
                    },
                    new PaymentNorth
                    {
                        Id = "ZALOPAY",
                        Name = "ZALOPAY",
                        Image = null
                    },
                    new PaymentNorth
                    {
                        Id = "CASH",
                        Name = "CASH",
                        Image = null
                    });
            });

            modelBuilder.Entity<RoleNorth>(entity =>
            {
                entity.HasData(
                    new RoleNorth
                    {
                        Id = "Staff",
                        Name = "STAFF",
                    },
                    new RoleNorth
                    {
                        Id = "Admin",
                        Name = "ADMIN",
                    });
            });

            modelBuilder.Entity<ShippingMethodNorth>(entity =>
            {
                entity.HasData(
                    new ShippingMethodNorth
                    {
                        Id = "GRAB",
                        Name = "GRAB",
                    },
                    new ShippingMethodNorth
                    {
                        Id = "BE",
                        Name = "BE",
                    },
                    new ShippingMethodNorth
                    {
                        Id = "SHOPEEFOOD",
                        Name = "SHOPEE FOOD",
                    });
            });
        }
    }
}
