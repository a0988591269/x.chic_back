using MyApp.Domain.Contexts;
using MyApp.Domain.Interfaces;

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