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
    }
}