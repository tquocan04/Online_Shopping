using AutoMapper;
using DTOs.DTOs;
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
            
            CreateMap<District, DistrictDTO>();
            CreateMap<DistrictDTO, District>();

            CreateMap<Payment, PaymentDTO>();
            CreateMap<PaymentDTO, Payment>();
            
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<RequestProduct, ProductDTO>().ReverseMap();
            CreateMap<RequestProduct, Product>().ReverseMap();
        }
    }
}
