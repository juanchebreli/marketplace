using marketplace.Helpers;
using marketplace.Helpers.Exceptions;
using marketplace.Helpers.Factory;
using marketplace.Helpers.Interfaces;
using marketplace.Helpers.Jwt;
using marketplace.Repositories;
using marketplace.Repositories.Interfaces;
using marketplace.Services;
using marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace marketplace.Configurations
{
    public static class StartupConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductOnSaleService, ProductOnSaleService>();
            services.AddScoped<IPaymentService, PaymentService>();
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
            services.AddScoped<PaymentMethodFactory>();
        }

        public static void AddJWT(this IServiceCollection services, IConfigurationSection jwtSection)
        {
            services.Configure<JWT>(jwtSection);
            var JWT = jwtSection.Get<JWT>();
            var key = Encoding.ASCII.GetBytes(JWT.SecretKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
