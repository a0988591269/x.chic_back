using Dapper;
using MyApp.Domain.Contexts;
using MyApp.Domain.Interfaces;

namespace MyApp.Infrastructure.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly IConnectionFactory _factory;

        public UserRoleRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<string>> GetRolesByUserId(long userId)
        {
            using var conn = _factory.GetConnection();

            var roles = await conn.QueryAsync<string>(
                @"
                    SELECT r.Name
                    FROM UserRoles ur
                    JOIN Roles r ON ur.RoleId = r.RoleId
                    WHERE ur.UserId = @userId
                ",
                new { userId });
            return roles;
        }
    }
}