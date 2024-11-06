using AutoMapper;
using DTOs.DTOs;
using Entities.Entities;

namespace Services
{
    public class AppMapper : Profile
    {
        public AppMapper() 
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
        }
    }
}
