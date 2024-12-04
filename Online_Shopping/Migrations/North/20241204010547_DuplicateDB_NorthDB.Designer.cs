﻿// <auto-generated />
using System;
using Entities.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Online_Shopping.Migrations.North
{
    [DbContext(typeof(ApplicationContextNorth))]
    [Migration("20241204010547_DuplicateDB_NorthDB")]
    partial class DuplicateDB_NorthDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Entities.North.AddressNorth", b =>
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
                });

            modelBuilder.Entity("Entities.Entities.North.BranchNorth", b =>
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

            modelBuilder.Entity("Entities.Entities.North.Branch_ProductNorth", b =>
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

            modelBuilder.Entity("Entities.Entities.North.CategoryNorth", b =>
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

            modelBuilder.Entity("Entities.Entities.North.CityNorth", b =>
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

            modelBuilder.Entity("Entities.Entities.North.CredentialNorth", b =>
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

            modelBuilder.Entity("Entities.Entities.North.CustomerNorth", b =>
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

            modelBuilder.Entity("Entities.Entities.North.DistrictNorth", b =>
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

            modelBuilder.Entity("Entities.Entities.North.EmployeeNorth", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
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
                });

            modelBuilder.Entity("Entities.Entities.North.ItemNorth", b =>
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

            modelBuilder.Entity("Entities.Entities.North.OrderNorth", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsCart")
                        .HasColumnType("bit");

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

            modelBuilder.Entity("Entities.Entities.North.PaymentNorth", b =>
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

            modelBuilder.Entity("Entities.Entities.North.ProductNorth", b =>
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

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Entities.Entities.North.RegionNorth", b =>
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

            modelBuilder.Entity("Entities.Entities.North.RoleNorth", b =>
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

            modelBuilder.Entity("Entities.Entities.North.ShippingMethodNorth", b =>
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

            modelBuilder.Entity("Entities.Entities.North.VoucherNorth", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("ExpiryDate")
                        .HasColumnType("date");

                    b.Property<decimal?>("MaxDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("MinOrderValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Percentage")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("Entities.Entities.North.AddressNorth", b =>
                {
                    b.HasOne("Entities.Entities.North.BranchNorth", "Branch")
                        .WithMany("Addresses")
                        .HasForeignKey("BranchId");

                    b.HasOne("Entities.Entities.North.CustomerNorth", "Customer")
                        .WithMany("Addresses")
                        .HasForeignKey("CustomerId");

                    b.HasOne("Entities.Entities.North.DistrictNorth", "District")
                        .WithMany("Addresses")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Entities.North.EmployeeNorth", "Employee")
                        .WithMany("Addresses")
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Branch");

                    b.Navigation("Customer");

                    b.Navigation("District");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Entities.Entities.North.Branch_ProductNorth", b =>
                {
                    b.HasOne("Entities.Entities.North.BranchNorth", "Branch")
                        .WithMany("Branch_Products")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Entities.North.ProductNorth", "Product")
                        .WithMany("BranchProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Entities.Entities.North.CityNorth", b =>
                {
                    b.HasOne("Entities.Entities.North.RegionNorth", "Region")
                        .WithMany("Cities")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Entities.Entities.North.CredentialNorth", b =>
                {
                    b.HasOne("Entities.Entities.North.CustomerNorth", "Customer")
                        .WithMany("Credentials")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Entities.Entities.North.DistrictNorth", b =>
                {
                    b.HasOne("Entities.Entities.North.CityNorth", "City")
                        .WithMany("Districts")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Entities.Entities.North.EmployeeNorth", b =>
                {
                    b.HasOne("Entities.Entities.North.BranchNorth", "Branch")
                        .WithMany("Employees")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Entities.North.RoleNorth", "Role")
                        .WithMany("Employees")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Entities.Entities.North.ItemNorth", b =>
                {
                    b.HasOne("Entities.Entities.North.OrderNorth", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Entities.North.ProductNorth", "Product")
                        .WithMany("Items")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Entities.Entities.North.OrderNorth", b =>
                {
                    b.HasOne("Entities.Entities.North.CustomerNorth", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.Entities.North.PaymentNorth", "Payment")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentId");

                    b.HasOne("Entities.Entities.North.ShippingMethodNorth", "ShippingMethod")
                        .WithMany("Oders")
                        .HasForeignKey("ShippingMethodId");

                    b.HasOne("Entities.Entities.North.VoucherNorth", "Voucher")
                        .WithMany("Orders")
                        .HasForeignKey("VoucherId");

                    b.Navigation("Customer");

                    b.Navigation("Payment");

                    b.Navigation("ShippingMethod");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("Entities.Entities.North.ProductNorth", b =>
                {
                    b.HasOne("Entities.Entities.North.CategoryNorth", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Entities.Entities.North.BranchNorth", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Branch_Products");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Entities.Entities.North.CategoryNorth", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Entities.Entities.North.CityNorth", b =>
                {
                    b.Navigation("Districts");
                });

            modelBuilder.Entity("Entities.Entities.North.CustomerNorth", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Credentials");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Entities.Entities.North.DistrictNorth", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("Entities.Entities.North.EmployeeNorth", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("Entities.Entities.North.OrderNorth", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Entities.Entities.North.PaymentNorth", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Entities.Entities.North.ProductNorth", b =>
                {
                    b.Navigation("BranchProducts");

                    b.Navigation("Items");
                });

            modelBuilder.Entity("Entities.Entities.North.RegionNorth", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("Entities.Entities.North.RoleNorth", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Entities.Entities.North.ShippingMethodNorth", b =>
                {
                    b.Navigation("Oders");
                });

            modelBuilder.Entity("Entities.Entities.North.VoucherNorth", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
