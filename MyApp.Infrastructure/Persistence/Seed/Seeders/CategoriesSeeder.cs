using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyApp.Infrastructure.Persistence.Contexts;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Persistence.Seed.Seeders
{
    public static class CategoriesSeeder
    {
        public static async Task Run(Contexts.AppDbContext context, ILogger logger)
        {
            if (await context.Categories.AnyAsync()) return;

            var categories = new List<Category>
            {
                new() { CategoryEngName = "Top", CategoryName = "上衣", Description = "上半身服飾", Slug = "x.chic.Top" },
                new() { CategoryEngName = "Bottom", CategoryName = "下身", Description = "褲裝與裙類", Slug = "x.chic.Bottom" },
                new() { CategoryEngName = "Accessory", CategoryName = "配件", Description = "帽子、包包、飾品", Slug = "x.chic.Accessory" }
            };

            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();

            logger.LogInformation("📁 Seeded Categories");
        }
    }
}