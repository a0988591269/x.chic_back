using Dapper;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class RoleRepository:BaseRepository, IRoleRepository
    {
        public RoleRepository(IConnectionFactory factory) : base(factory, DatabaseKey.Default) { }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await WithConnectionAsync(conn =>
                conn.QueryAsync<Role>(" SELECT * FROM Roles "));
        }
    }
}
