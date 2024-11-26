using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.Repositories;
using Repository.Contracts;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;
using Services.Services;
using System.Text;

namespace Online_Shopping.Extensions
{
    public static class ServiceExtension 
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IShippingService, ShippingService>();

            return services;
        }
        public static IServiceCollection ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IAddressRepo, AddressRepo>();
            services.AddScoped<IBranchRepo, BranchRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<IRegionRepo, RegionRepo>();
            services.AddScoped<ICityRepo, CityRepo>();
            services.AddScoped<IPaymentRepo, PaymentRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<ILoginRepo,LoginRepo>();
            services.AddScoped<IOrderRepo,OrderRepo>();
            services.AddScoped<IShippingRepo, ShippingRepo>();
            services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));

            return services;
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSetting = configuration.GetSection("Jwt");
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters   //tham so xac thuc cho jwt
                {
                    //cap token: true-> dich vu, false->tu cap
                    ValidateIssuer = false,
                    //ValidIssuer = jwtSetting["Issuer"],

                    ValidateAudience = false,
                    //ValidAudience = jwtSetting["Audience"],

                    ClockSkew = TimeSpan.Zero, // bo tg chenh lech
                    ValidateLifetime = true,    //xac thuc thoi gian ton tai cua token

                    //ky vao token
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting["Secret"])),
                    ValidateIssuerSigningKey = true
                };
            });
        }
    }
}
