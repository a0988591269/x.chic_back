using Dapper;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
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

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            using var conn = _factory.GetConnection();

            return await conn.QueryAsync<Category>(" SELECT * FROM Categories ");
        }

        public async Task<Category?> GetBySlug(string slug)
        {
            using var conn = _factory.GetConnection();

            var sql = @" SELECT * FROM Categories WHERE Slug = @Slug ";
            return await conn.QueryFirstOrDefaultAsync<Category>(sql, new { Slug = slug });
        }
    }
}
