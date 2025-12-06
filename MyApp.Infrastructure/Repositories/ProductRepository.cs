using MyApp.Domain.Enums;
using MyApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;

namespace MyApp.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConnectionFactory _factory;

        public ProductRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using var conn = _factory.GetConnection();

            return await conn.QueryAsync<Product>(" SELECT * FROM Products; ");
        }

        public async Task<Product?> GetByProductId(long productId)
        {
            using var conn = _factory.GetConnection();

            var sql = @" SELECT * FROM Products WHERE ProductId = @ProductId; ";
            return await conn.QueryFirstOrDefaultAsync<Product>(sql, new { ProductId = productId });
        }
    }
}