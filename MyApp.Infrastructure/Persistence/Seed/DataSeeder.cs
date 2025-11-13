using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyApp.Infrastructure.Persistence.Contexts;
using MyApp.Infrastructure.Persistence.Seed.Extensions;
using System.Diagnostics;

namespace MyApp.Infrastructure.Persistence.Seed
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext context, ILogger logger)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            logger.LogInformation("🔧 [DB Seeder] Starting database migration...");
            await context.Database.MigrateAsync();

            try
            {
                logger.LogInformation("✅ [DB Seeder] Begin seeding process...");

                await LogStepAsync("Roles & Users", async () => await context.SeedUsersAndRoles(logger), logger);
                await LogStepAsync("Categories", async () => await context.SeedCategories(logger), logger);
                await LogStepAsync("Products", async () => await context.SeedProducts(logger), logger);

                stopwatch.Stop();
                logger.LogInformation("🎉 [DB Seeder] Completed successfully in {Elapsed} ms.", stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                logger.LogError(ex, "💥 [DB Seeder] Failed after {Elapsed} ms.", stopwatch.ElapsedMilliseconds);
                throw;
            }
        }

        private static async Task LogStepAsync(string stepName, Func<Task> action, ILogger logger)
        {
            var stepWatch = Stopwatch.StartNew();
            logger.LogInformation("➡️ [DB Seeder] Seeding {StepName}...", stepName);

            try
            {
                await action();
                stepWatch.Stop();
                logger.LogInformation("✅ [DB Seeder] {StepName} done in {Elapsed} ms.", stepName, stepWatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                stepWatch.Stop();
                logger.LogError(ex, "❌ [DB Seeder] Error seeding {StepName} after {Elapsed} ms.", stepName, stepWatch.ElapsedMilliseconds);
                throw;
            }
        }
    }
}