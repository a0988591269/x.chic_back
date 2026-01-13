using Dapper;
using MyApp.Domain.Contexts;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContext _db;
        private readonly IConnectionFactory _factory;

        public UserRepository(IDbContext db, IConnectionFactory factory)
        {
            _db = db;
            _factory = factory;
        }

        public async Task AddAsync(User user, CancellationToken token)
        {
            await _db.Users.AddAsync(user, token);
            _db.SaveChanges();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            using var conn = _factory.GetConnection();

            var user = await conn.QuerySingleOrDefaultAsync<User>(
                @"
                    SELECT * 
                    FROM Users 
                    WHERE Email = @email AND Status = 1
                ",
               new { email });

            return user;
        }

        /// <summary>
        /// 判斷 Email 是否唯一 (即尚未被註冊)
        /// </summary>
        public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken token)
        {
            using var conn = _factory.GetConnection();

            var user = await conn.ExecuteScalarAsync<int>(

                @"
                    SELECT COUNT(1) 
                    FROM Users 
                    WHERE Email = @email
                ",
                new { email });

            return user == 0;
        }
    }
}