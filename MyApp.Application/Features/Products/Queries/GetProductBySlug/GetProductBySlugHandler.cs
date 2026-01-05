using Dapper;
using MediatR;
using MyApp.Application.Commons.Results;
using MyApp.Domain.Contexts;

namespace MyApp.Application.Features.Products.Queries.GetProductBySlug
{
    public class GetProductBySlugHandler : IRequestHandler<GetProductBySlugQuery, Result<IEnumerable<GetProductBySlugDto>>>
    {
        private readonly IConnectionFactory _factory;

        public GetProductBySlugHandler(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<Result<IEnumerable<GetProductBySlugDto>>> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
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
                         WHERE c.Slug = @Slug AND p.IsActive = 1 ";
            var product = await conn.QueryAsync<GetProductBySlugDto>(sql, new { request.Slug });

            if (product == null)
            {
                return Result<IEnumerable<GetProductBySlugDto>>.NotFound();
            }

            return Result<IEnumerable<GetProductBySlugDto>>.Success(product);
        }
    }
}
