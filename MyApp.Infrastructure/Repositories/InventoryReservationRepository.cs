using Dapper;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repositories
{
    public class InventoryReservationRepository : IInventoryReservationRepository
    {
        private readonly IConnectionFactory _factory;

        public InventoryReservationRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<InventoryReservation>> GetAllAsync()
        {
            using var conn = _factory.GetConnection();

            return await conn.QueryAsync<InventoryReservation>(" SELECT * FROM InventoryReservations ");
        }
    }
}
