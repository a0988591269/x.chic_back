using Dapper;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly IConnectionFactory _factory;

        public RolePermissionRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<string>> GetRolePermissionByUserId(long UserId)
        {
            using var conn = _factory.GetConnection();

            var permissions = await conn.QueryAsync<string>(
                @"
                    SELECT DISTINCT rp.Permission
                    FROM UserRoles ur
                    JOIN RolePermissions rp ON ur.RoleId = rp.RoleId
                    WHERE ur.UserId = @UserId
                ",
                new { UserId });

            return permissions;
        }
    }
}