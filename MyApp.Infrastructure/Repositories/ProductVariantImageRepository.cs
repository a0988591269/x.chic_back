using Dapper;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class ProductVariantImageRepository: BaseRepository, IProductVariantImageRepository
    {
        public ProductVariantImageRepository(IConnectionFactory factory) : base(factory, DatabaseKey.Default) { }

        public async Task<IEnumerable<ProductVariantImage>> GetAllAsync()
        {
            return await WithConnectionAsync(conn =>
                conn.QueryAsync<ProductVariantImage>(" SELECT * FROM ProductVariantImages "));
        }
    }
}
