using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class ProductVariantOptionValueRepository : IProductVariantOptionValueRepository
    {
        private readonly IConnectionFactory _factory;

        public ProductVariantOptionValueRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }
    }
}