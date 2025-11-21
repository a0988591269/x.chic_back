using Dapper;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repositories
{
    public class ProductOptionValueRepository : BaseRepository, IProductOptionValueRepository
    {
        public ProductOptionValueRepository(IDapperConnectionFactory factory) : base(factory, DatabaseKey.Default) { }

        public async Task<IEnumerable<ProductOptionValue>> GetAllAsync()
        {
            return await WithConnectionAsync(conn =>
                conn.QueryAsync<ProductOptionValue>(" SELECT * FROM ProductOptionValues "));
        }
    }
}
