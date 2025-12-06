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
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly IConnectionFactory _factory;

        public OrderItemRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            using var conn = _factory.GetConnection();

            return await conn.QueryAsync<OrderItem>(" SELECT * FROM OrderItems ");
        }
    }
}