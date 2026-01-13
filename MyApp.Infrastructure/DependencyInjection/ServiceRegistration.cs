using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Commons.Interfaces.Authentication;
using MyApp.Application.Commons.Interfaces.JWT;
using MyApp.Domain.Contexts;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Authentication;
using MyApp.Infrastructure.JWT;
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
            // EF Core DbContext 注入
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            // EF Core 注入介面
            services.AddScoped<IDbContext>(provider =>
                provider.GetRequiredService<AppDbContext>());

            // Dapper 注入介面
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();

            // 注入 Repository
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();

            services.Configure<JwtOptions>(config.GetSection("Jwt"));
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            // PasswordHasher 注入
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
