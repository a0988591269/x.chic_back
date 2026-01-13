using Dapper;
using MyApp.Domain.Contexts;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;

namespace MyApp.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IConnectionFactory _factory;

        public RoleRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<string>> GetRolesByUserId(long UserId)
        {
            using var conn = _factory.GetConnection();

            var roles = await conn.QueryAsync<string>(
                @"
                    SELECT r.Name
                    FROM UserRoles ur
                    JOIN Roles r ON ur.RoleId = r.RoleId
                    WHERE ur.UserId = @UserId
                ",
                new { UserId });
            return roles;
        }
    }
}