using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly IConnectionFactory _factory;

        public OrderItemRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }
    }
}