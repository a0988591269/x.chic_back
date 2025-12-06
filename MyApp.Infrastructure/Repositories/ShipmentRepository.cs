using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

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