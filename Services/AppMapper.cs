﻿using AutoMapper;
using DTOs.DTOs;
using DTOs.MongoDb;
using DTOs.Request;
using Entities.Entities;

namespace Services
{
    public class AppMapper : Profile
    {
        public AppMapper() 
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CategoryDTO, RequestCategory>().ReverseMap();
            CreateMap<Category, RequestCategory>().ReverseMap();

            CreateMap<City, CityDTO>();
            CreateMap<CityDTO, City>();

            CreateMap<RegionDTO, Region>().ReverseMap();

            CreateMap<District, DistrictDTO>().ReverseMap();

            CreateMap<Payment, PaymentDTO>();
            CreateMap<PaymentDTO, Payment>();
            
            CreateMap<ProductMetadata, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<RequestProduct, ProductDTO>().ReverseMap();
            CreateMap<RequestProduct, Product>()
                .ForMember(dest => dest.Image,
                            opt => opt.Ignore());
            
            CreateMap<CustomerDTO, CustomerMetadata>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<RequestCustomer, CustomerDTO>().ReverseMap();
            CreateMap<RequestCustomer, Customer>()
                .ForMember( dest => dest.Dob,
                            opt => opt.Ignore())
                .ForMember( dest => dest.Picture,
                            opt => opt.Ignore())
                .ReverseMap();
            CreateMap<RequestCustomer, Address>();
            CreateMap<RequestCustomer, DistributedCustomer>()
                .ForMember(dest => dest.Picture,
                            opt => opt.Ignore());

            CreateMap<Order, OrderCartDTO>().ReverseMap();
            CreateMap<Order, OrderBillDTO>().ReverseMap();
            CreateMap<Item, ItemDTO>().ReverseMap();

            CreateMap<Product, ItemDTO>()
                .ForMember(dest => dest.ProductId,
                            opt => opt.MapFrom(src => src.Id));


            CreateMap<ShippingMethod, ShippingDTO>().ReverseMap();

            CreateMap<RequestBranch, BranchDTO>().ReverseMap();
            CreateMap<BranchDTO, Branch>().ReverseMap();
            CreateMap<RequestBranch, Branch>();
            CreateMap<RequestBranch, Address>();
            CreateMap<BranchDTO, Address>().ReverseMap();

            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            //CreateMap<Address, EmployeeDTO>()
            //    .ForMember(dest => dest.Id,
            //                opt => opt.MapFrom(src => src.ObjectId));
            CreateMap<RequestEmployee, EmployeeDTO>().ReverseMap();
            CreateMap<RequestEmployee, Employee>()
                .ForMember(dest => dest.Dob,
                            opt => opt.Ignore());

            CreateMap<RequestEmployee, Address>()
                .ForMember(dest => dest.BranchId,
                            opt => opt.Ignore());
            
            CreateMap<RequestVoucher, Voucher>()
                .ForMember(dest => dest.Id,
                            opt => opt.Ignore())
                .ForMember(dest => dest.Code,
                            opt => opt.MapFrom(src => src.Code.ToUpper()));
            CreateMap<VoucherDTO, Voucher>().ReverseMap();
            CreateMap<VoucherDTO, RequestVoucher>().ReverseMap();

        }
    }
}