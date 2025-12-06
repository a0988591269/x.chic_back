using Dapper;
using MyApp.Application.Interfaces;
using MyApp.Application.Services.Categories.DTOs;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Application.Services.Categories.Queries
{
    public class CategoryQueryService : ICategoryService
    {
        private readonly IConnectionFactory _factory;


        public CategoryQueryService(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            using var conn = _factory.GetConnection();

            return await conn.QueryAsync<CategoryDto>(" SELECT * FROM Categories ");
        }
    }
}
