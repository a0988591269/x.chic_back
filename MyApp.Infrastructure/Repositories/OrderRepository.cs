using MyApp.Domain.Contexts;
using MyApp.Domain.Interfaces;

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