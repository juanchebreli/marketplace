using marketplace.Helpers;
using marketplace.Helpers.Exceptions;
using marketplace.Helpers.Interfaces;
using marketplace.Repositories;
using marketplace.Repositories.Interfaces;
using marketplace.Services;
using marketplace.Services.Interfaces;

namespace marketplace.Configurations
{
    public static class StartupConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductOnSaleService, ProductOnSaleService>();
            services.AddScoped<ICashMethodService, CashMethodService>();
            services.AddScoped<ICardMethodService, CardMethodService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductOnSaleRepository, ProductOnSaleRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
        }

        public static void AddHelpers(this IServiceCollection services)
        {
            services.AddScoped<ICryptoEngine, CryptoEngine>();
            services.AddScoped<IJwtMiddleware, JwtMiddleware>();
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
