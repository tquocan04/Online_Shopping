﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Online_Shopping.Context;

#nullable disable

namespace Online_Shopping.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DistrictId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fd781de2-93a7-421c-a0dc-8f7a6279c04e"),
                            DistrictId = new Guid("bfb4be74-f5ed-4e67-94ba-7ee067a2098d"),
                            EmployeeId = new Guid("32aecf7c-3670-42ba-bd24-c4173b2452df"),
                            IsDefault = true,
                            Street = "2012004"
                        });
                });

            modelBuilder.Entity("Entities.Entities.Branch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("Entities.Entities.BranchProduct", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "BranchId");

                    b.HasIndex("BranchId");

                    b.ToTable("BranchProducts");
                });

            modelBuilder.Entity("Entities.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Entities.Entities.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RegionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.HasIndex("RegionId");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"),
                            Name = "Hà Nội",
                            RegionId = "Bac"
                        },
                        new
                        {
                            Id = new Guid("fc446281-359c-46ec-a2b9-bf9f26014f88"),
                            Name = "Đà Nẵng",
                            RegionId = "Trung"
                        },
                        new
                        {
                            Id = new Guid("6f624665-053e-45d2-8dd6-42baa124b481"),
                            Name = "Hồ Chí Minh",
                            RegionId = "Nam"
                        });
                });

            modelBuilder.Entity("Entities.Entities.Credential", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Provider")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Credentials");
                });

            modelBuilder.Entity("Entities.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("Dob")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Entities.Entities.District", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Districts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("bfb4be74-f5ed-4e67-94ba-7ee067a2098d"),
                            CityId = new Guid("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"),
                            Name = "Ba Đình"
                        },
                        new
                        {
                            Id = new Guid("b999d3eb-a753-49f4-897f-4c37002e1302"),
                            CityId = new Guid("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"),
                            Name = "Cầu Giấy"
                        },
                        new
                        {
                            Id = new Guid("2e8e7f13-ca6a-42c7-a1a0-fc5b4c872b3f"),
                            CityId = new Guid("fc446281-359c-46ec-a2b9-bf9f26014f88"),
                            Name = "Hải Châu"
                        },
                        new
                        {
                            Id = new Guid("9fc84eab-708a-49a2-b819-06d0629e560a"),
                            CityId = new Guid("fc446281-359c-46ec-a2b9-bf9f26014f88"),
                            Name = "Hoàng Sa"
                        },
                        new
                        {
                            Id = new Guid("53b0b29e-7b2c-4d4b-accc-f693ce746539"),
                            CityId = new Guid("6f624665-053e-45d2-8dd6-42baa124b481"),
                            Name = "Quận 1"
                        },
                        new
                        {
                            Id = new Guid("c893d9cf-7b2d-4ebc-9c65-3f78e0fce6bb"),
                            CityId = new Guid("6f624665-053e-45d2-8dd6-42baa124b481"),
                            Name = "Quận 5"
                        },
                        new
                        {
                            Id = new Guid("f9be5b5e-847d-47e9-847a-0150c4f608e1"),
                            CityId = new Guid("6f624665-053e-45d2-8dd6-42baa124b481"),
                            Name = "Quận 10"
                        });
                });

            modelBuilder.Entity("Entities.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("Dob")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("RoleId");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = new Guid("32aecf7c-3670-42ba-bd24-c4173b2452df"),
                            Dob = new DateOnly(2004, 1, 20),
                            Email = "adminn@gmail.com",
                            Gender = "Nam",
                            Name = "Admin",
                            Password = "admin123",
                            PhoneNumber = "0939771198",
                            RoleId = "Admin",
                            Username = "adminn"
                        });
                });

            modelBuilder.Entity("Entities.Entities.Item", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Entities.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsCart")
                        .HasColumnType("bit");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ShippingMethodId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("VoucherId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("ShippingMethodId");

                    b.HasIndex("VoucherId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Entities.Entities.Payment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Payments");

                    b.HasData(
                        new
                        {
                            Id = "MOMO",
                            Name = "MOMO"
                        },
                        new
                        {
                            Id = "ZALOPAY",
                            Name = "ZALOPAY"
                        },
                        new
                        {
                            Id = "CASH",
                            Name = "CASH"
                        });
                });

            modelBuilder.Entity("Entities.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Entities.Entities.Region", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = "Bac",
                            Name = "Miền Bắc"
                        },
                        new
                        {
                            Id = "Trung",
                            Name = "Miền Trung"
                        },
                        new
                        {
                            Id = "Nam",
                            Name = "Miền Nam"
                        });
                });

            modelBuilder.Entity("Entities.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = "Staff",
                            Name = "STAFF"
                        },
                        new
                        {
                            Id = "Admin",
                            Name = "ADMIN"
                        });
                });

            modelBuilder.Entity("Entities.Entities.ShippingMethod", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ShippingMethods");

                    b.HasData(
                        new
                        {
                            Id = "GRAB",
                            Name = "GRAB"
                        },
                        new
                        {
                            Id = "BE",
                            Name = "BE"
                        },
                        new
                        {
                            Id = "SHOPEEFOOD",
                            Name = "SHOPEE FOOD"
                        });
                });

            modelBuilder.Entity("Entities.Entities.Voucher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("ExpiryDate")
                        .HasColumnType("date");

                    b.Property<decimal>("MaxDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("MinOrderValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Percentage")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("Entities.Entities.Address", b =>
                {
                    b.HasOne("Entities.Entities.Branch", "Branch")
                        .WithMany("Addresses")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.Entities.Customer", "Customer")
                        .WithMany("Addresses")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.Entities.District", "District")
                        .WithMany("Addresses")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Entities.Employee", "Employee")
                        .WithMany("Addresses")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Branch");

                    b.Navigation("Customer");

                    b.Navigation("District");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Entities.Entities.BranchProduct", b =>
                {
                    b.HasOne("Entities.Entities.Branch", "Branch")
                        .WithMany("Branch_Products")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Entities.Product", "Product")
                        .WithMany("BranchProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Entities.Entities.City", b =>
                {
                    b.HasOne("Entities.Entities.Region", "Region")
                        .WithMany("Cities")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Entities.Entities.Credential", b =>
                {
                    b.HasOne("Entities.Entities.Customer", "Customer")
                        .WithMany("Credentials")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Entities.Entities.District", b =>
                {
                    b.HasOne("Entities.Entities.City", "City")
                        .WithMany("Districts")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Entities.Entities.Employee", b =>
                {
                    b.HasOne("Entities.Entities.Branch", "Branch")
                        .WithMany("Employees")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Entities.Entities.Role", "Role")
                        .WithMany("Employees")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Entities.Entities.Item", b =>
                {
                    b.HasOne("Entities.Entities.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Entities.Product", "Product")
                        .WithMany("Items")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Entities.Entities.Order", b =>
                {
                    b.HasOne("Entities.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.Entities.Payment", "Payment")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentId");

                    b.HasOne("Entities.Entities.ShippingMethod", "ShippingMethod")
                        .WithMany("Oders")
                        .HasForeignKey("ShippingMethodId");

                    b.HasOne("Entities.Entities.Voucher", "Voucher")
                        .WithMany("Orders")
                        .HasForeignKey("VoucherId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Customer");

                    b.Navigation("Payment");

                    b.Navigation("ShippingMethod");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("Entities.Entities.Product", b =>
                {
                    b.HasOne("Entities.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Entities.Entities.Branch", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Branch_Products");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Entities.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Entities.Entities.City", b =>
                {
                    b.Navigation("Districts");
                });

            modelBuilder.Entity("Entities.Entities.Customer", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Credentials");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Entities.Entities.District", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("Entities.Entities.Employee", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("Entities.Entities.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Entities.Entities.Payment", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Entities.Entities.Product", b =>
                {
                    b.Navigation("BranchProducts");

                    b.Navigation("Items");
                });

            modelBuilder.Entity("Entities.Entities.Region", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("Entities.Entities.Role", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Entities.Entities.ShippingMethod", b =>
                {
                    b.Navigation("Oders");
                });

            modelBuilder.Entity("Entities.Entities.Voucher", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
