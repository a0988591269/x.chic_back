using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyApp.Infrastructure.Persistence.Contexts;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Persistence.Seed.Seeders
{
    public static class ProductsSeeder
    {
        public static async Task Run(AppDbContext context, ILogger logger)
        {
            if (await context.Products.AnyAsync()) return;

            var topCategoryId = await context.Categories
                .Where(c => c.CategoryEngName == "Top")
                .Select(c => c.CategoryId)
                .FirstAsync();

            // --- 主商品 ---
            var product = new Product
            {
                ProductName = "Basic Oversized Tee",
                ShortDescription = "潮感寬版短袖上衣",
                LongDescription = "高磅數棉質、舒適不悶熱，日常百搭。",
                CategoryId = topCategoryId,
                IsActive = true
            };

            context.Products.Add(product);
            await context.SaveChangesAsync();

            // --- Options ---
            var colorOpt = new ProductOption { ProductId = product.ProductId, Name = "Color", SortOrder = 1 };
            var sizeOpt = new ProductOption { ProductId = product.ProductId, Name = "Size", SortOrder = 2 };
            context.ProductOptions.AddRange(colorOpt, sizeOpt);
            await context.SaveChangesAsync();

            // --- Option Values ---
            var colors = new[]
            {
                new ProductOptionValue { ProductOptionId = colorOpt.ProductOptionId, Value = "White" },
                new ProductOptionValue { ProductOptionId = colorOpt.ProductOptionId, Value = "Black" }
            };
            var sizes = new[]
            {
                new ProductOptionValue { ProductOptionId = sizeOpt.ProductOptionId, Value = "M" },
                new ProductOptionValue { ProductOptionId = sizeOpt.ProductOptionId, Value = "L" }
            };
            context.ProductOptionValues.AddRange(colors);
            context.ProductOptionValues.AddRange(sizes);
            await context.SaveChangesAsync();

            // --- Variants (White×M, White×L, Black×M, Black×L) ---
            var variants = new List<ProductVariant>();
            foreach (var color in colors)
                foreach (var size in sizes)
                {
                    variants.Add(new ProductVariant
                    {
                        ProductId = product.ProductId,
                        Sku = $"{product.ProductId}-{color.Value}-{size.Value}",
                        Price = 490m,
                        StockQty = 20,
                        IsActive = true
                    });
                }

            context.ProductVariants.AddRange(variants);
            await context.SaveChangesAsync();

            // --- Optional: Images ---
            var firstVariantId = variants.First().ProductVariantId;
            context.ProductVariantImages.Add(new ProductVariantImage
            {
                ProductVariantId = firstVariantId,
                ImageUrl = "https://via.placeholder.com/400x400?text=Tee",
                SortOrder = 1
            });
            await context.SaveChangesAsync();

            logger.LogInformation("🛍️ Seeded Products + Variants + Images");
        }
    }
}