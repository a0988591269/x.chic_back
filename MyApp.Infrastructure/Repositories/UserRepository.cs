using Dapper;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionFactory _factory;

        public UserRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<User?> GetUserByEmail(string Email)
        {
            using var conn = _factory.GetConnection();

            var user = await conn.QuerySingleOrDefaultAsync<User>(
                @"
                    SELECT * 
                    FROM Users 
                    WHERE Email = @Email AND Status = 1
                ",
               new { Email });

            return user;
        }
    }
}