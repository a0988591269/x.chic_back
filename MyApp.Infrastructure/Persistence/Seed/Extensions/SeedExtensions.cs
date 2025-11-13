using Microsoft.Extensions.Logging;
using MyApp.Infrastructure.Persistence.Contexts;
using MyApp.Infrastructure.Persistence.Seed.Seeders;

namespace MyApp.Infrastructure.Persistence.Seed.Extensions
{
    public static class SeedExtensions
    {
        public static async Task SeedUsersAndRoles(this AppDbContext context, ILogger logger)
            => await UsersAndRolesSeeder.Run(context, logger);

        public static async Task SeedCategories(this AppDbContext context, ILogger logger)
            => await CategoriesSeeder.Run(context, logger);

        public static async Task SeedProducts(this AppDbContext context, ILogger logger)
            => await ProductsSeeder.Run(context, logger);
    }
}