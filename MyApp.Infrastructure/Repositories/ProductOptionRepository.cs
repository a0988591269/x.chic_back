using Dapper;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class ProductOptionRepository : IProductOptionRepository
    {
        private readonly IConnectionFactory _factory;

        public ProductOptionRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<ProductOption>> GetAllAsync()
        {
            using var conn = _factory.GetConnection();
            return await conn.QueryAsync<ProductOption>(" SELECT * FROM ProductOptions ");
        }
    }
}