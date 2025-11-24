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
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(IDapperConnectionFactory factory) : base(factory, DatabaseKey.Default) { }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await WithConnectionAsync(conn =>
                conn.QueryAsync<Product>(" SELECT * FROM Products; "));
        }

        public async Task<Product?> GetByProductId(long productId)
        {
            var sql = @" SELECT * FROM Products WHERE ProductId = @ProductId; ";
            return await WithConnectionAsync(conn =>
                conn.QueryFirstOrDefaultAsync<Product>(sql, new { ProductId = productId }));
        }
    }
}
