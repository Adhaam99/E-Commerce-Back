using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;

namespace Service
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(typeof(Service.AssemplyReference).Assembly);
            Services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();
            
            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<Func<IProductService>>(Provider =>
                () => Provider.GetRequiredService<IProductService>());

            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<Func<IBasketService>>(Provider =>
                () => Provider.GetRequiredService<IBasketService>());

            Services.AddScoped<IAuthenticationService, AutheniticationService>();
            Services.AddScoped<Func<IAuthenticationService>>(Provider =>
                () => Provider.GetRequiredService<IAuthenticationService>());

            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<Func<IOrderService>>(Provider =>
                () => Provider.GetRequiredService<IOrderService>());
            return Services;
        }
    }
}
