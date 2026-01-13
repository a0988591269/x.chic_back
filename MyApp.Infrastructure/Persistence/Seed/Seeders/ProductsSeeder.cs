using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Persistence.Seed.Seeders
{
    public static class ProductsSeeder
    {
        public static async Task Run(Contexts.AppDbContext context, ILogger logger)
        {
            if (await context.Products.AnyAsync()) return;

            var topCategoryId = await context.Categories
                .Where(c => c.CategoryEngName == "Top")
                .Select(c => c.CategoryId)
                .FirstAsync();

            // --- 主商品 ---
            var product = new Product
            {
                ProductName = "𝓐𝓮𝓼𝓹𝓪金旼炡𝓦𝓲𝓷𝓽𝓮𝓻同款白色背心",
                ShortDescription = "<p>採用 <strong>100% 美國棉</strong></p>\r\n<ul>\r\n  <li>柔軟透氣超舒適</li>\r\n  <li>雙面磨絨超保暖</li>\r\n</ul>",
                LongDescription = "𝓦𝓲𝓷𝓽𝓮𝓻身上這件夢幻白色綁帶上衣，完全就是精靈系少女 的代表單品！ 淡淡的白雲色帶有一種柔和純真的感覺，不僅顯白，還能營造清新甜美的氛圍。",
                CategoryId = topCategoryId,
                IsActive = true,
                IsHot = true,
                IsNew = true,
                IsRecommended = true,
            };

            context.Products.Add(product);
            await context.SaveChangesAsync();

            // --- Options ---
            var colorOpt = new ProductOption { ProductId = product.ProductId, Name = "顏色", SortOrder = 1 };
            var sizeOpt = new ProductOption { ProductId = product.ProductId, Name = "尺寸", SortOrder = 2 };
            context.ProductOptions.AddRange(colorOpt, sizeOpt);
            await context.SaveChangesAsync();

            // --- Option Values ---
            var colors = new[]
            {
                new ProductOptionValue { ProductOptionId = colorOpt.ProductOptionId, Value = "白色" },
                new ProductOptionValue { ProductOptionId = colorOpt.ProductOptionId, Value = "黑色" }
            };
            var sizes = new[]
            {
                new ProductOptionValue { ProductOptionId = sizeOpt.ProductOptionId, Value = "M" },
                new ProductOptionValue { ProductOptionId = sizeOpt.ProductOptionId, Value = "L" }
            };
            context.ProductOptionValues.AddRange(colors);
            context.ProductOptionValues.AddRange(sizes);
            await context.SaveChangesAsync();

            // --- Variants (白色×M, 白色×L, 黑色×M, 黑色×L) ---
            Random rnd = new Random();
            var variants = new List<ProductVariant>();
            foreach (var color in colors)
            {
                foreach (var size in sizes)
                {
                    variants.Add(new ProductVariant
                    {
                        ProductId = product.ProductId,
                        Sku = $"{product.ProductId}-{color.Value}-{size.Value}",
                        Price = 490m,
                        DiscountPrice = 390m,
                        StockQty = rnd.Next(0, 20),
                        IsActive = true
                    });
                }
            }

            context.ProductVariants.AddRange(variants);
            await context.SaveChangesAsync();

            foreach (var variant in variants)
            {
                // 連結 Variant 與 Option Values
                context.ProductVariantOptionValues.AddRange(
                    new ProductVariantOptionValue
                    {
                        ProductVariantId = variant.ProductVariantId,
                        ProductOptionValueId = colors.First(c => c.Value == variant.Sku.Split('-')[1]).ProductOptionValueId
                    },
                    new ProductVariantOptionValue
                    {
                        ProductVariantId = variant.ProductVariantId,
                        ProductOptionValueId = sizes.First(s => s.Value == variant.Sku.Split('-')[2]).ProductOptionValueId
                    }
                );
            }
            await context.SaveChangesAsync();

            // --- Optional: Images ---
            var firstVariantId = variants.First().ProductVariantId;
            context.ProductVariantImages.Add(new ProductVariantImage
            {
                ProductVariantId = firstVariantId,
                ImageUrl = "/images/Test.png",
                SortOrder = 1
            });
            await context.SaveChangesAsync();

            logger.LogInformation("🛍️ Seeded Products + Variants + Images");
        }
    }
}