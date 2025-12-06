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
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly IConnectionFactory _factory;

        public UserRoleRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            using var conn = _factory.GetConnection();

            return await conn.QueryAsync<UserRole>(" SELECT * FROM UserRoles ");
        }
    }
}