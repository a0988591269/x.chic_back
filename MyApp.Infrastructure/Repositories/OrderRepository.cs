using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class OrderRepository :  IOrderRepository
    {
        private readonly IConnectionFactory _factory;

        public OrderRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }
    }
}