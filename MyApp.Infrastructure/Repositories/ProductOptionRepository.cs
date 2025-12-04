using Dapper;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class ProductOptionRepository : BaseRepository, IProductOptionRepository
    {
        public ProductOptionRepository(IConnectionFactory factory) : base(factory, DatabaseKey.Default) { }

        public async Task<IEnumerable<ProductOption>> GetAllAsync()
        {
            return await WithConnectionAsync(conn =>
                conn.QueryAsync<ProductOption>(" SELECT * FROM ProductOptions "));
        }
    }
}
