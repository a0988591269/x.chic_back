using Dapper;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class ProductOptionValueRepository : IProductOptionValueRepository
    {
        private readonly IConnectionFactory _factory;

        public ProductOptionValueRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<ProductOptionValue>> GetAllAsync()
        {
            using var conn = _factory.GetConnection();

            return await conn.QueryAsync<ProductOptionValue>(" SELECT * FROM ProductOptionValues ");
        }
    }
}