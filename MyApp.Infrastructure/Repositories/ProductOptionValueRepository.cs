using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class ProductOptionValueRepository : IProductOptionValueRepository
    {
        private readonly IConnectionFactory _factory;

        public ProductOptionValueRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }
    }
}