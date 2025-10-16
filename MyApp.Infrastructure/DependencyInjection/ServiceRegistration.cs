using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Infrastructure.Persistence;

namespace MyApp.Infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddConnection(this IServiceCollection services, IConfiguration config)
        {
            services.AddDefaultConn(config);
            //services.AddScoped<IPlayerRepository, PlayerRepository>();
            return services;
        }
    }
}
