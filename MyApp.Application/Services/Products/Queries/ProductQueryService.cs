using Dapper;
using MyApp.Application.Interfaces;
using MyApp.Application.Services.Products.DTOs;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Application.Services.Categories.Queries
{
    public class ProductQueryService : IProductService
    {
        private readonly IConnectionFactory _factory;

        public ProductQueryService(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<ProductDto>> GetBySlug(string slug)
        {
            using var conn = _factory.GetConnection();

            var sql = @" SELECT p.* FROM Categories c
                         LEFT JOIN Products p
                         ON c.CategoryId = p.CategoryId
                         WHERE c.Slug = @Slug ";
            return await conn.QueryAsync<ProductDto>(sql, new { Slug = slug });
        }
    }
}
