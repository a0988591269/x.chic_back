using MyApp.Domain.Contexts;
using MyApp.Domain.Interfaces;

namespace MyApp.Infrastructure.Repositories
{
    public class InventoryReservationRepository : IInventoryReservationRepository
    {
        private readonly IConnectionFactory _factory;

        public InventoryReservationRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }
    }
}
