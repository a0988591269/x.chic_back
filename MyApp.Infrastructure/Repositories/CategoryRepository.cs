using MyApp.Domain.Contexts;
using MyApp.Domain.Interfaces;

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