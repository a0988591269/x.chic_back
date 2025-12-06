using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence;
using MyApp.Infrastructure.Persistence.Contexts;
using MyApp.Infrastructure.Repositories;

namespace MyApp.Infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDapperSingle(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbConnection(config);
            //services.AddScoped<IPlayerRepository, PlayerRepository>();
            return services;
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            // EF Core 建表
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            // Dapper 查資料
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
