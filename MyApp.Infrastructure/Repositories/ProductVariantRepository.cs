using Dapper;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repositories
{
    public class ProductVariantRepository : IProductVariantRepository
    {
        private readonly IConnectionFactory _factory;

        public ProductVariantRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<ProductVariant>> GetAllAsync()
        {
            using var conn = _factory.GetConnection();

            return await conn.QueryAsync<ProductVariant>(" SELECT * FROM ProductVariants ");
        }

        public async Task<IEnumerable<ProductVariant>> GetByProductId(long productId)
        {
            using var conn = _factory.GetConnection();

            return await conn.QueryAsync<ProductVariant>(" SELECT * FROM ProductVariants WHERE productId");
        }
    }
}