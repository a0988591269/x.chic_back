using MyApp.Domain.Contexts;
using MyApp.Domain.Interfaces;

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