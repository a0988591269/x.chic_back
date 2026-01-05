using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConnectionFactory _factory;
        private readonly IDbContext _dbContext;

        public ProductRepository(IConnectionFactory factory, IDbContext dbContext)
        {
            _factory = factory;
            _dbContext = dbContext;
        }
    }
}