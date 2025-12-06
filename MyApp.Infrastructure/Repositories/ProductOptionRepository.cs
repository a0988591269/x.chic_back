using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class ProductOptionRepository : IProductOptionRepository
    {
        private readonly IConnectionFactory _factory;

        public ProductOptionRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }
    }
}