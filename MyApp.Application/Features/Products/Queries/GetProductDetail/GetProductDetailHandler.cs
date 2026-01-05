using Dapper;
using MediatR;
using MyApp.Application.Commons.Results;

namespace MyApp.Application.Features.Products.Queries.GetProductDetail
{
    public class GetProductDetailHandler : IRequestHandler<GetProductDetailQuery, Result<GetProductDetailDto>>
    {
        private readonly IConnectionFactory _factory;

        public GetProductDetailHandler(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<Result<GetProductDetailDto>> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
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
            var query = await conn.QueryMultipleAsync(sql, new { request.Id });

            var product = await query.ReadSingleOrDefaultAsync<GetProductDetailDto>();
            if(product == null)
            {
                return Result<GetProductDetailDto>.NotFound();
            }
            var options = (await query.ReadAsync<ProductOptionDto>()).ToList();
            // 使用 dynamic 暫存以便包含 ProductOptionId
            var optionValues = (await query.ReadAsync<dynamic>()).ToList();
            var variants = (await query.ReadAsync<VariantDto>()).ToList();
            // 關聯表資料
            var variantValueMap = (await query.ReadAsync<dynamic>()).ToList();
            var variantImages = (await query.ReadAsync<dynamic>()).ToList();

            // --- 開始組裝資料 (Memory Assembly) ---

            // A. 組裝 Options 與 Values
            foreach (var opt in options)
            {
                opt.Values = optionValues
                    .Where(v => v.ProductOptionId == opt.Id)
                    .Select(v => new OptionValueDto { Id = v.Id, Value = v.Value })
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

            return Result<GetProductDetailDto>.Success(product);
        }
    }
}
