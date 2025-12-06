using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

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
