using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyApp.Domain.Authentication;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Persistence.Seed.Extensions
{
    public static class HostExtensions
    {
        /// <summary>
        /// 自動執行 DB Migration + Seeding
        /// </summary>
        public static async Task SeedDataAsync(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("DB Seeder");
            var passwordHasher = services.GetRequiredService<IPasswordHasher>();

            try
            {
                var context = services.GetRequiredService<AppDbContext>();
                await DataSeeder.SeedAsync(context, logger, passwordHasher);
                logger.LogInformation("✅ Database migration & seeding completed successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "❌ Database seeding failed during startup.");
            }
        }
    }
}