using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class OutboxEventRepository : IOutboxEventRepository
    {
        private readonly IConnectionFactory _factory;

        public OutboxEventRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }
    }
}