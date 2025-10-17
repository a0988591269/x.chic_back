using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Infrastructure.Persistence;

namespace MyApp.Infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbConnection(config);
            //services.AddScoped<IPlayerRepository, PlayerRepository>();
            return services;
        }

        public static IServiceCollection AddMultipleContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            //services.AddScoped<IPlayerRepository, PlayerRepository>();
            return services;
        }
    }
}
