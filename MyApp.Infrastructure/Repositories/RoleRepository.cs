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
    }
}