using Dapper;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        /// <summary>
        /// base 傳入兩個參數
        /// </summary>
        /// <param name="factory"></param>
        public CategoryRepository(IDapperConnectionFactory factory) : base(factory, DatabaseKey.Default) { }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await WithConnectionAsync(conn =>
                conn.QueryAsync<Category>(" SELECT * FROM Categories "));
        }

        public async Task<Category?> GetByCategoryId(int categoryId)
        {
            return await WithConnectionAsync(conn =>
                conn.QueryFirstOrDefaultAsync<Category>(" SELECT * FROM Categories WHERE CategoryId = @CategoryId ", new { CategoryId = categoryId }));
        }

        public async Task<Category?> GetBySlug(string slug)
        {
            return await WithConnectionAsync(conn =>
                conn.QueryFirstOrDefaultAsync<Category>(" SELECT * FROM Categories WHERE Slug = @Slug ", new { Slug = slug }));
        }
    }
}
