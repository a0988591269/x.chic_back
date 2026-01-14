using Dapper;
using MyApp.Domain.Contexts;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;

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