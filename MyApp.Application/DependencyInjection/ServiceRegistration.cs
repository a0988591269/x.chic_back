using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interfaces;
using MyApp.Application.Services.Categories.Queries;

namespace MyApp.Application.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ICategoryService, CategoryQueryService>();
            services.AddScoped<IProductService, ProductQueryService>();
            return services;
        }
    }
}
