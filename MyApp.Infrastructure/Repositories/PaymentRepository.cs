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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IConnectionFactory _factory;

        public PaymentRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            using var conn = _factory.GetConnection();

            return await conn.QueryAsync<Payment>(" SELECT * FROM Payments ");
        }
    }
}