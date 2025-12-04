using Dapper;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence.Contexts;

namespace MyApp.Infrastructure.Repositories
{
    public class RefundRepository: BaseRepository, IRefundRepository
    {
        public RefundRepository(IConnectionFactory factory) : base(factory, DatabaseKey.Default) { }

        public async Task<IEnumerable<Refund>> GetAllAsync()
        {
            return await WithConnectionAsync(conn =>
                conn.QueryAsync<Refund>(" SELECT * FROM Refunds "));
        }
    }
}
