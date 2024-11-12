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

            CreateMap<District, DistrictDTO>().ReverseMap();

            CreateMap<Payment, PaymentDTO>();
            CreateMap<PaymentDTO, Payment>();
            
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<RequestProduct, ProductDTO>().ReverseMap();
            CreateMap<RequestProduct, Product>().ReverseMap();
            
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<RequestUser, UserDTO>().ReverseMap();
            CreateMap<RequestUser, User>()
                .ForMember( dest => dest.Name,
                            opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember( dest => dest.Dob,
                            opt => opt.Ignore())
                .ReverseMap();

            CreateMap<User, Customer>()
                .ForMember(dest => dest.Id,
                            opt => opt.MapFrom(src => src.Id));
            CreateMap<RequestUser, Cus_Address>()
                .ForMember(dest => dest.Street,
                            opt => opt.MapFrom(src => src.Street));
            CreateMap<User, Cus_Address>()
                .ForMember(dest => dest.UserId,
                           opt => opt.MapFrom(src => src.Id));
        }
    }
}
