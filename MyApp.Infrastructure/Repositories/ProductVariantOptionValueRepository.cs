using MyApp.Domain.Contexts;
using MyApp.Domain.Interfaces;

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