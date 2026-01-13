using Dapper;
using MyApp.Application.Commons.Results;
using MyApp.Application.Features.Categories.Queries.GetCategory;
using MyApp.Domain.Contexts;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;

namespace MyApp.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IConnectionFactory _factory;

        public CategoryRepository(IConnectionFactory factory) {
            _factory = factory;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            using var conn = _factory.GetConnection();

            var sql = @" SELECT * FROM Categories ";
            var category = await conn.QueryAsync<Category>(sql);

            return category;
        }
    }
}