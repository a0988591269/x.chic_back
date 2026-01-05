using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionFactory _factory;

        public UserRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }
    }
}