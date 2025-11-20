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
    public class RolePermissionRepository : BaseRepository, IRolePermissionRepository
    {
        public RolePermissionRepository(IDapperConnectionFactory factory) : base(factory, DatabaseKey.Default) { }

        public async Task<IEnumerable<RolePermission>> GetAllAsync()
        {
            return await WithConnectionAsync(conn =>
                conn.QueryAsync<RolePermission>(" SELECT * FROM RolePermissions "));
        }
    }
}
