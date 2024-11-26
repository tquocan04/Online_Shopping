using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using DTOs.Responses;
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
            
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<RequestProduct, ProductDTO>().ReverseMap();
            CreateMap<RequestProduct, Product>().ReverseMap();
            
            CreateMap<RequestUser, CustomerDTO>().ReverseMap();
            CreateMap<RequestUser, Customer>()
                .ForMember( dest => dest.Name,
                            opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember( dest => dest.Dob,
                            opt => opt.Ignore())
                .ReverseMap();
            CreateMap<RequestUser, Address>();

            CreateMap<Order, OrderCartDTO>().ReverseMap();
            CreateMap<Item, ItemDTO>().ReverseMap();
            CreateMap<ShippingMethod, ShippingDTO>().ReverseMap();

            CreateMap<RequestBranch, BranchDTO>().ReverseMap();
            CreateMap<BranchDTO, Branch>().ReverseMap();
            CreateMap<RequestBranch, Branch>();
            CreateMap<RequestBranch, Address>();
            CreateMap<BranchDTO, Address>().ReverseMap();
        }
    }
}