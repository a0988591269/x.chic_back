using MyApp.Domain.Contexts;
using MyApp.Domain.Interfaces;

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