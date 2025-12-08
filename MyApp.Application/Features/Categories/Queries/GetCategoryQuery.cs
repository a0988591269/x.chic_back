using Dapper;
using MyApp.Application.Features.Categories.Queries;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Application.Services.Categories.Queries
{
    public class GetCategoryQuery
    {
        private readonly IConnectionFactory _factory;


        public GetCategoryQuery(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<GetCategoryDto>> GetAllAsync()
        {
            using var conn = _factory.GetConnection();

            return await conn.QueryAsync<GetCategoryDto>(" SELECT * FROM Categories ");
        }
    }
}
