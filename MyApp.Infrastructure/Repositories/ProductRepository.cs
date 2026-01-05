using MyApp.Domain.Contexts;
using MyApp.Domain.Interfaces;

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