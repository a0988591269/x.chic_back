using MyApp.Domain.Contexts;
using MyApp.Domain.Interfaces;

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