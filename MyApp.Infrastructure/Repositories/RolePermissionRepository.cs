using MyApp.Domain.Contexts;
using MyApp.Domain.Interfaces;

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