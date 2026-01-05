using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly IConnectionFactory _factory;

        public UserRoleRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }
    }
}