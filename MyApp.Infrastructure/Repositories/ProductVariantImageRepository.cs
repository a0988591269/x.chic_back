using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class ProductVariantImageRepository : IProductVariantImageRepository
    {
        private readonly IConnectionFactory _factory;

        public ProductVariantImageRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }
    }
}