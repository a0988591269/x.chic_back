using MyApp.Domain.Contexts;
using MyApp.Domain.Interfaces;

namespace MyApp.Infrastructure.Repositories
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly IConnectionFactory _factory;

        public ShipmentRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }
    }
}