using Repositories;
using Repositories.Repositories;
using Repository.Contracts;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;
using Services.Services;

namespace Online_Shopping.Extensions
{
    public static class ExtensionServices 
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
        public static IServiceCollection ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<ICityRepo, CityRepo>();
            services.AddScoped<IPaymentRepo, PaymentRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<IUserRepo,UserRepo>();
            services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
            return services;
        }
    }
}
