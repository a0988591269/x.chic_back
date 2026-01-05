using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IConnectionFactory _factory;

        public CategoryRepository(IConnectionFactory factory) {
            _factory = factory;
        }
    }
}