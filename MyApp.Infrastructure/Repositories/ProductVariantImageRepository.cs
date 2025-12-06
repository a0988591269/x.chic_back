using Dapper;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class ProductVariantImageRepository : IProductVariantImageRepository
    {
        private readonly IConnectionFactory _factory;

        public ProductVariantImageRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<ProductVariantImage>> GetAllAsync()
        {
            using var conn = _factory.GetConnection();

            return await conn.QueryAsync<ProductVariantImage>(" SELECT * FROM ProductVariantImages ");
        }
    }
}