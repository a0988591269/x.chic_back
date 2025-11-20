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
    public class UserRoleRepository : BaseRepository, IUserRoleRepository
    {
        public UserRoleRepository(IDapperConnectionFactory factory) : base(factory, DatabaseKey.Default) { }
        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return await WithConnectionAsync(conn =>
                conn.QueryAsync<UserRole>(" SELECT * FROM UserRoles "));
        }
    }
}
