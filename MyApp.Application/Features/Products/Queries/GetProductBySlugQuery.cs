using Dapper;
using MediatR;
using MyApp.Application.Commons.Results;
using MyApp.Application.Features.Products.Queries;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Application.Services.Categories.Queries
{
    public class GetProductBySlugQuery : IRequest<Result<GetProductBySlugDto>>
    {
        private readonly IConnectionFactory _factory;

        public GetProductBySlugQuery(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<GetProductBySlugDto>> GetBySlug(string slug)
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
            return await conn.QueryAsync<GetProductBySlugDto>(sql, new { Slug = slug });
        }
    }
}
