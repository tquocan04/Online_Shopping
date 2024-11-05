using Repositories.Repositories;
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
            return services;
        }
        public static IServiceCollection ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            return services;
        }
    }
}
