using MyApp.Domain.Contexts;
using MyApp.Domain.Interfaces;

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