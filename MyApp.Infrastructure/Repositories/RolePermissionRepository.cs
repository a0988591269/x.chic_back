using Dapper;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repositories
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly IConnectionFactory _factory;

        public RolePermissionRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<RolePermission>> GetAllAsync()
        {
            using var conn = _factory.GetConnection();

            return await conn.QueryAsync<RolePermission>(" SELECT * FROM RolePermissions ");
        }
    }
}