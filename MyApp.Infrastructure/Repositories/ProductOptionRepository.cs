using MyApp.Domain.Contexts;
using MyApp.Domain.Interfaces;

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