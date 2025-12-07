using Dapper;
using MyApp.Application.Interfaces;
using MyApp.Application.Services.ProductVariants.DTOs;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Application.Services.ProductVariants.Queries
{
    public class ProductVariantQueryService : IProductVariantsService
    {
        private readonly IConnectionFactory _factory;

        public ProductVariantQueryService(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<ProductVariantDto>> GetByProductId(long productId)
        {
            using var conn = _factory.GetConnection();

            var sql = @" SELECT * FROM ProductVariants
                         WHERE ProductId = @ProductId ";
            return await conn.QueryAsync<ProductVariantDto>(sql, new { ProductId = productId });
        }
    }
}