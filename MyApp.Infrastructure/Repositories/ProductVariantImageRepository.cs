using MyApp.Domain.Contexts;
using MyApp.Domain.Interfaces;

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