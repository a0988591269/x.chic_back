using Dapper;
using MyApp.Domain.Interfaces;
using MyApp.Domain.Models.Products;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConnectionFactory _factory;

        public ProductRepository(IConnectionFactory factory, IDbContext dbContext)
        {
            _factory = factory;
        }

        public async Task<GetProductDetailModel?> GetProductById(long Id)
        {
            using var conn = _factory.GetConnection();

            var sql = @"
                -- 1. 商品主檔
                SELECT ProductId as Id, ProductName as Name, ShortDescription as Summary, 
                       LongDescription as Description, 0 as Price, NULL as DiscountPrice, -- 價格由變體決定或顯示預設
                       TotalSales as Sales, Rating 
                FROM Products WHERE ProductId = @Id;

                -- 2. 選項 (Options)
                SELECT ProductOptionId as Id, Name FROM ProductOptions 
                WHERE ProductId = @Id ORDER BY SortOrder;

                -- 3. 選項值 (OptionValues)
                SELECT v.ProductOptionValueId as Id, v.Value, v.ProductOptionId 
                FROM ProductOptionValues v
                JOIN ProductOptions o ON v.ProductOptionId = o.ProductOptionId
                WHERE o.ProductId = @Id;

                -- 4. 變體 (Variants)
                SELECT ProductVariantId as Id, Sku, Price, DiscountPrice, StockQty 
                FROM ProductVariants WHERE ProductId = @Id AND IsActive = 1;

                -- 5. 變體與規格值的關聯 (關鍵!)
                SELECT pvov.ProductVariantId, pvov.ProductOptionValueId
                FROM ProductVariantOptionValues pvov
                JOIN ProductVariants pv ON pvov.ProductVariantId = pv.ProductVariantId
                WHERE pv.ProductId = @Id;

                -- 6. 變體圖片
                SELECT ProductVariantId, ImageUrl FROM ProductVariantImages 
                WHERE ProductVariantId IN (SELECT ProductVariantId FROM ProductVariants WHERE ProductId = @Id)
                ORDER BY SortOrder;
            ";
            var query = await conn.QueryMultipleAsync(sql, new { Id });

            var product = await query.ReadSingleOrDefaultAsync<GetProductDetailModel>();
            if (product == null) return null;

            var options = (await query.ReadAsync<ProductOptionModel>()).ToList();
            // 使用 dynamic 暫存以便包含 ProductOptionId
            var optionValues = (await query.ReadAsync<dynamic>()).ToList();
            var variants = (await query.ReadAsync<VariantModel>()).ToList();
            // 關聯表資料
            var variantValueMap = (await query.ReadAsync<dynamic>()).ToList();
            var variantImages = (await query.ReadAsync<dynamic>()).ToList();

            // --- 開始組裝資料 (Memory Assembly) ---

            // A. 組裝 Options 與 Values
            foreach (var opt in options)
            {
                opt.Values = optionValues
                    .Where(v => v.ProductOptionId == opt.Id)
                    .Select(v => new OptionValueModel { Id = v.Id, Value = v.Value })
                    .ToList();
            }
            product.Options = options;

            // B. 組裝 Variants 的 OptionValueIds 與 Images
            foreach (var v in variants)
            {
                // 找出該變體對應的所有 OptionValueId
                v.OptionValueIds = variantValueMap
                    .Where(x => x.ProductVariantId == v.Id)
                    .Select(x => (int)x.ProductOptionValueId)
                    .ToList();

                // 找出圖片
                v.Images = variantImages
                    .Where(x => x.ProductVariantId == v.Id)
                    .Select(x => (string)x.ImageUrl)
                    .ToList();
            }
            product.Variants = variants;

            // C. 補上商品預設價格 (通常取第一個變體的價格，或由業務邏輯決定)
            if (variants.Any())
            {
                product.Price = variants.Min(v => v.Price);
                product.DiscountPrice = variants.Min(v => v.DiscountPrice);
            }

            return product;
        }

        public async Task<IEnumerable<GetProductBySlugModel>> GetProductBySlug(string slug)
        {
            using var conn = _factory.GetConnection();

            var sql = @" SELECT p.*, pv.*, pvi.* FROM Categories c
                         LEFT JOIN Products p
                         ON c.CategoryId = p.CategoryId
                         CROSS APPLY (
                             SELECT TOP 1 ProductVariantId, Sku, Price, DiscountPrice, StockQty
                             FROM ProductVariants pv
                             WHERE pv.ProductId = p.ProductId AND IsActive = 1
                             ORDER BY pv.Price ASC
                         ) pv
                         CROSS APPLY (
                             SELECT TOP 1 ImageUrl
                             FROM ProductVariantImages pvi
	                         WHERE pv.ProductVariantId = pvi.ProductVariantId
                             ORDER BY pvi.SortOrder ASC
                         ) pvi
                         WHERE c.Slug = @slug AND p.IsActive = 1 ";

            var product = await conn.QueryAsync<GetProductBySlugModel>(sql, new { slug });
            return product;
        }
    }
}