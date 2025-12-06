using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class ProductVariantRepository : IProductVariantRepository
    {
        private readonly IConnectionFactory _factory;

        public ProductVariantRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }
    }
}