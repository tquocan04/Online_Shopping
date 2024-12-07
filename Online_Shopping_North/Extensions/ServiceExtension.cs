using Online_Shopping_North.Repositories;
using Online_Shopping_North.Repository.Contracts;
using Online_Shopping_North.Service.Contracts;
using Online_Shopping_North.Services;

namespace Online_Shopping_North.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<ICategoryService, CategoryService>();
            //services.AddScoped<ICityService, CityService>();
            //services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();
            //services.AddScoped<ILoginService, LoginService>();
            //services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IOrderService, OrderService>();
            //services.AddScoped<IShippingService, ShippingService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IVoucherService, VoucherService>();

            services.AddScoped(typeof(IAddressService<>), typeof(AddressService<>));

            return services;
        }
        public static IServiceCollection ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IAddressRepo, AddressRepo>();
            services.AddScoped<IBranchRepo, BranchRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            //services.AddScoped<IRegionRepo, RegionRepo>();
            //services.AddScoped<ICityRepo, CityRepo>();
            //services.AddScoped<IPaymentRepo, PaymentRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<ICustomerRepo, CustomerRepo>();
            //services.AddScoped<ILoginRepo, LoginRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            //services.AddScoped<IShippingRepo, ShippingRepo>();
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            services.AddScoped<IVoucherRepo, VoucherRepo>();
            //services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));

            return services;
        }
    }
}
