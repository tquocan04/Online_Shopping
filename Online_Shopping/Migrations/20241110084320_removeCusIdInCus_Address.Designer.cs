﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Online_Shopping.Context;

#nullable disable

namespace Online_Shopping.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20241110084320_removeCusIdInCus_Address")]
    partial class removeCusIdInCus_Address
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Entities.Bill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Order_date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PaymentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("PaymentId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("Entities.Entities.Buy_Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BillId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "CustomerId", "BillId");

                    b.HasIndex("BillId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Buy_Products");
                });

            modelBuilder.Entity("Entities.Entities.Cart", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "CustomerId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Carts");
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

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"),
                            Name = "Hà Nội"
                        },
                        new
                        {
                            Id = new Guid("fc446281-359c-46ec-a2b9-bf9f26014f88"),
                            Name = "Đà Nẵng"
                        },
                        new
                        {
                            Id = new Guid("6f624665-053e-45d2-8dd6-42baa124b481"),
                            Name = "Hồ Chí Minh"
                        });
                });

            modelBuilder.Entity("Entities.Entities.Cus_Address", b =>
                {
                    b.Property<Guid>("DistrictId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DistrictId", "Street", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("CusAddresses");
                });

            modelBuilder.Entity("Entities.Entities.Cus_Voucher", b =>
                {
                    b.Property<Guid>("VoucherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.HasKey("VoucherId", "CustomerId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CusVouchers");
                });

            modelBuilder.Entity("Entities.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

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
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Entities.Entities.Import_Bill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Order_date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("ImportBills");
                });

            modelBuilder.Entity("Entities.Entities.Import_Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ImportBillId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SupplierId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "ImportBillId", "SupplierId");

                    b.HasIndex("ImportBillId");

                    b.HasIndex("SupplierId");

                    b.ToTable("ImportProducts");
                });

            modelBuilder.Entity("Entities.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Payments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("70d99884-b132-4913-9cc9-bf4b50885ec3"),
                            Name = "Momo"
                        },
                        new
                        {
                            Id = new Guid("5f750901-00ba-4658-99b1-17b6173e8ce6"),
                            Name = "ZaloPay"
                        },
                        new
                        {
                            Id = new Guid("79ed495e-52c0-4446-a347-64c913fad40f"),
                            Name = "Cash"
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

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Entities.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("11ae3454-c2a0-4b13-ae85-7e0063d1391f"),
                            Name = "STAFF"
                        },
                        new
                        {
                            Id = new Guid("64442b7e-5955-469f-bbc9-2e6df052cb9c"),
                            Name = "ADMIN"
                        });
                });

            modelBuilder.Entity("Entities.Entities.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DistrictId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Entities.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("Dob")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone_number")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Entities.Entities.Voucher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Code")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("ExpiryDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<double>("MinOrder")
                        .HasColumnType("float");

                    b.Property<int>("Percentage")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("Entities.Entities.Bill", b =>
                {
                    b.HasOne("Entities.Entities.Payment", "Payment")
                        .WithMany("Bills")
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("Entities.Entities.Buy_Product", b =>
                {
                    b.HasOne("Entities.Entities.Bill", "Bill")
                        .WithMany("BuyProducts")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Entities.Customer", "Customer")
                        .WithMany("BuyProducts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Entities.Product", "Product")
                        .WithMany("BuyProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Customer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Entities.Entities.Cart", b =>
                {
                    b.HasOne("Entities.Entities.Customer", "Customer")
                        .WithMany("Carts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Entities.Product", "Product")
                        .WithMany("Carts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Entities.Entities.Cus_Address", b =>
                {
                    b.HasOne("Entities.Entities.District", "District")
                        .WithMany("CusAddresses")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Entities.User", "User")
                        .WithMany("CusAddresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("District");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.Entities.Cus_Voucher", b =>
                {
                    b.HasOne("Entities.Entities.Customer", "Customer")
                        .WithMany("CusVouchers")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Entities.Voucher", "Voucher")
                        .WithMany("Cus_Vouchers")
                        .HasForeignKey("VoucherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("Entities.Entities.Customer", b =>
                {
                    b.HasOne("Entities.Entities.User", "User")
                        .WithOne("Customer")
                        .HasForeignKey("Entities.Entities.Customer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
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
                    b.HasOne("Entities.Entities.Role", "Role")
                        .WithMany("Employees")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Entities.User", "User")
                        .WithOne("Employee")
                        .HasForeignKey("Entities.Entities.Employee", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.Entities.Import_Product", b =>
                {
                    b.HasOne("Entities.Entities.Import_Bill", "ImportBill")
                        .WithMany("ImportProducts")
                        .HasForeignKey("ImportBillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Entities.Product", "Product")
                        .WithMany("ImportProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Entities.Supplier", "Supplier")
                        .WithMany("ImportProducts")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ImportBill");

                    b.Navigation("Product");

                    b.Navigation("Supplier");
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

            modelBuilder.Entity("Entities.Entities.Supplier", b =>
                {
                    b.HasOne("Entities.Entities.District", "District")
                        .WithMany("Suppliers")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("District");
                });

            modelBuilder.Entity("Entities.Entities.Bill", b =>
                {
                    b.Navigation("BuyProducts");
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
                    b.Navigation("BuyProducts");

                    b.Navigation("Carts");

                    b.Navigation("CusVouchers");
                });

            modelBuilder.Entity("Entities.Entities.District", b =>
                {
                    b.Navigation("CusAddresses");

                    b.Navigation("Suppliers");
                });

            modelBuilder.Entity("Entities.Entities.Import_Bill", b =>
                {
                    b.Navigation("ImportProducts");
                });

            modelBuilder.Entity("Entities.Entities.Payment", b =>
                {
                    b.Navigation("Bills");
                });

            modelBuilder.Entity("Entities.Entities.Product", b =>
                {
                    b.Navigation("BuyProducts");

                    b.Navigation("Carts");

                    b.Navigation("ImportProducts");
                });

            modelBuilder.Entity("Entities.Entities.Role", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Entities.Entities.Supplier", b =>
                {
                    b.Navigation("ImportProducts");
                });

            modelBuilder.Entity("Entities.Entities.User", b =>
                {
                    b.Navigation("CusAddresses");

                    b.Navigation("Customer");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Entities.Entities.Voucher", b =>
                {
                    b.Navigation("Cus_Vouchers");
                });
#pragma warning restore 612, 618
        }
    }
}
