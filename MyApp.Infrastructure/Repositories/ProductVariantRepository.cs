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
    public class ProductVariantRepository : BaseRepository, IProductVariantRepository
    {
        public ProductVariantRepository(IDapperConnectionFactory factory) : base(factory, DatabaseKey.Default) { }

        public async Task<IEnumerable<ProductVariant>> GetAllAsync()
        {
            return await WithConnectionAsync(conn =>
                conn.QueryAsync<ProductVariant>(" SELECT * FROM ProductVariants "));
        }

        public async Task<IEnumerable<ProductVariant>> GetByProductId(long productId)
        {
            return await WithConnectionAsync(conn =>
                conn.QueryAsync<ProductVariant>(" SELECT * FROM ProductVariants WHERE productId"));
        }
    }
}
