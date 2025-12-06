using Dapper;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class RefundRepository : IRefundRepository
    {
        private readonly IConnectionFactory _factory;

        public RefundRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<Refund>> GetAllAsync()
        {
            using var conn = _factory.GetConnection();

            return await conn.QueryAsync<Refund>(" SELECT * FROM Refunds ");
        }
    }
}