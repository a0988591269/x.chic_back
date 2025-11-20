using MyApp.Domain.Entities;

namespace MyApp.Domain.Interfaces
{
    public interface IShipmentRepository
    {
        Task<IEnumerable<Shipment>> GetAllAsync();
    }
}