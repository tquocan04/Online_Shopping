﻿using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Online_Shopping.Context
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
                    //miền bắc
                    new City 
                    { 
                        Id = Guid.Parse("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"), 
                        Name = "Hà Nội",
                        RegionId = "Bac"
                    },
                    new City 
                    { 
                        Id = Guid.Parse("0bdea930-da3d-40c6-97cd-b3969f8014c7"), 
                        Name = "Hải Phòng",
                        RegionId = "Bac"
                    },
                    new City 
                    { 
                        Id = Guid.Parse("4a486645-052b-4a56-bb36-75c7e876ae2d"), 
                        Name = "Hải Dương",
                        RegionId = "Bac"
                    },
                    new City 
                    { 
                        Id = Guid.Parse("2df88e32-3919-494d-b489-dbf4258fc245"), 
                        Name = "Phú Thọ",
                        RegionId = "Bac"
                    },
                    new City 
                    { 
                        Id = Guid.Parse("14b0bd4d-27af-496e-aa9c-3e1d532f5038"), 
                        Name = "Quảng Ninh",
                        RegionId = "Bac"
                    },

                    //miền trung
                    new City 
                    { 
                        Id = Guid.Parse("fc446281-359c-46ec-a2b9-bf9f26014f88"), 
                        Name = "Đà Nẵng",
                        RegionId = "Trung"
                    },
                    new City 
                    { 
                        Id = Guid.Parse("3321ed88-441b-4121-9ead-e154544185e1"), 
                        Name = "Thừa-Thiên Huế",
                        RegionId = "Trung"
                    },
                    new City 
                    { 
                        Id = Guid.Parse("aec5e588-017c-4da1-91e8-b8bc1888056e"), 
                        Name = "Quảng Nam",
                        RegionId = "Trung"
                    },
                    new City 
                    { 
                        Id = Guid.Parse("a41ba56a-b53b-42f6-8c56-04dcbbde7905"), 
                        Name = "Quảng Trị",
                        RegionId = "Trung"
                    },

                    //miền nam
                    new City 
                    { 
                        Id = Guid.Parse("b40f6c23-15f7-460c-8f94-fdcbe33cda68"), 
                        Name = "Bình Dương",
                        RegionId = "Nam"
                    },
                    new City 
                    { 
                        Id = Guid.Parse("bbf96ba4-7836-4c53-af1a-e3e572f31ebf"), 
                        Name = "Bà Rịa Vũng Tàu",
                        RegionId = "Nam"
                    },
                    new City 
                    { 
                        Id = Guid.Parse("acd51ba8-d6e3-4110-831e-5147f8fe2c96"), 
                        Name = "Đồng Nai",
                        RegionId = "Nam"
                    },
                    new City 
                    { 
                        Id = Guid.Parse("6f624665-053e-45d2-8dd6-42baa124b481"), 
                        Name = "Hồ Chí Minh",
                        RegionId = "Nam"
                    },
                    new City
                    {
                        Id = Guid.Parse("ad453439-a309-42a3-917c-d6aaa67ac9ca"),
                        Name = "Long An",
                        RegionId = "Nam"
                    }
                );
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasIndex(d => d.Name).IsUnique();
                entity.HasData(
                    //Quảng Ninh
                    new District
                    {
                        Id = Guid.Parse("4af1f7fa-cd35-4940-b2f9-8811ca9a2b75"),
                        CityId = Guid.Parse("14b0bd4d-27af-496e-aa9c-3e1d532f5038"),
                        Name = "Cẩm Phả"
                    },
                    new District
                    {
                        Id = Guid.Parse("4af1f7fa-cd35-4940-b2f9-8811ca9a2b75"),
                        CityId = Guid.Parse("14b0bd4d-27af-496e-aa9c-3e1d532f5038"),
                        Name = "Cẩm Phả"
                    },
                    new District
                    {
                        Id = Guid.Parse("4af1f7fa-cd35-4940-b2f9-8811ca9a2b75"),
                        CityId = Guid.Parse("14b0bd4d-27af-496e-aa9c-3e1d532f5038"),
                        Name = "Cẩm Phả"
                    },
                    new District
                    {
                        Id = Guid.Parse("4af1f7fa-cd35-4940-b2f9-8811ca9a2b75"),
                        CityId = Guid.Parse("14b0bd4d-27af-496e-aa9c-3e1d532f5038"),
                        Name = "Cẩm Phả"
                    },
                    //Hải Dương
                    new District
                    {
                        Id = Guid.Parse("4af1f7fa-cd35-4940-b2f9-8811ca9a2b75"),
                        CityId = Guid.Parse("4a486645-052b-4a56-bb36-75c7e876ae2d"),
                        Name = "Chí Linh"
                    },
                    new District
                    {
                        Id = Guid.Parse("2f657b93-1d8b-4024-bfd9-827009d98c67"),
                        CityId = Guid.Parse("4a486645-052b-4a56-bb36-75c7e876ae2d"),
                        Name = "Hải Dương"
                    },
                    new District
                    {
                        Id = Guid.Parse("1f461ef4-7dbc-4f7c-b51e-a16a5cfca7d3"),
                        CityId = Guid.Parse("4a486645-052b-4a56-bb36-75c7e876ae2d"),
                        Name = "Gia Lộc"
                    },
                    //Phú Thọ
                    new District
                    {
                        Id = Guid.Parse("eef65a95-e294-46a3-828f-5e44ca4b2c77"),
                        CityId = Guid.Parse("2df88e32-3919-494d-b489-dbf4258fc245"),
                        Name = "Hạ Hòa"
                    },
                    new District
                    {
                        Id = Guid.Parse("91d1c4dd-f364-412d-a7a2-8857f4b8a9c9"),
                        CityId = Guid.Parse("2df88e32-3919-494d-b489-dbf4258fc245"),
                        Name = "Thanh Thủy"
                    },
                    new District
                    {
                        Id = Guid.Parse("db4e7867-1156-44d9-825b-8f42ae9712fe"),
                        CityId = Guid.Parse("2df88e32-3919-494d-b489-dbf4258fc245"),
                        Name = "Yên Lập"
                    },
                    //Hải Phòng
                    new District
                    {
                        Id = Guid.Parse("9744402f-34c1-4555-baff-9450ab73303a"),
                        CityId = Guid.Parse("0bdea930-da3d-40c6-97cd-b3969f8014c7"),
                        Name = "Hồng Bàng"
                    },
                    new District
                    {
                        Id = Guid.Parse("033fbf94-3a48-4bec-a1d1-13f0dcca7ff2"),
                        CityId = Guid.Parse("0bdea930-da3d-40c6-97cd-b3969f8014c7"),
                        Name = "Lê Chân"
                    },
                    new District
                    {
                        Id = Guid.Parse("aabecff8-16d3-4298-839b-c5ec84ae49a3"),
                        CityId = Guid.Parse("0bdea930-da3d-40c6-97cd-b3969f8014c7"),
                        Name = "Kiến An"
                    },
                    //Hà Nội
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
                    new District
                    {
                        Id = Guid.Parse("b051a4a6-66ba-4220-95e4-59abd37d4e0b"),
                        CityId = Guid.Parse("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"),
                        Name = "Đống Đa"
                    },
                    new District
                    {
                        Id = Guid.Parse("83ba4e24-9a0f-4c6a-b822-29a1eb5f4d3f"),
                        CityId = Guid.Parse("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"),
                        Name = "Hoàn Kiếm"
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
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasData(
                    new Employee
                    {
                        Id = Guid.Parse("32aecf7c-3670-42ba-bd24-c4173b2452df"),
                        Name = "Admin",
                        Gender = "Nam",
                        Username = "adminn",
                        Email = "adminn@gmail.com",
                        Password = "admin123",
                        PhoneNumber = "0939771198",
                        Dob = new DateOnly(2004, 1, 20),
                        RoleId = "Admin",
                        BranchId = null,
                    });
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasData(
                    new Address
                    {
                        Id = Guid.Parse("fd781de2-93a7-421c-a0dc-8f7a6279c04e"),
                        EmployeeId = Guid.Parse("32aecf7c-3670-42ba-bd24-c4173b2452df"),
                        DistrictId = Guid.Parse("bfb4be74-f5ed-4e67-94ba-7ee067a2098d"),
                        Street = "2012004",
                        IsDefault = true,
                    }
                    );
            });
        }

    }
}
