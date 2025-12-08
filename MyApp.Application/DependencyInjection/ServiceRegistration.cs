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
            services.AddScoped<ICategoryRepository, GetCategoryQuery>();
            services.AddScoped<IProductRepository, GetProductBySlugQuery>();
            return services;
        }
    }
}
