using Dapper;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IConnectionFactory _factory;

        public RoleRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            using var conn = _factory.GetConnection();

            return await conn.QueryAsync<Role>(" SELECT * FROM Roles ");
        }
    }
}