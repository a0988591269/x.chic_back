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
    public class OutboxEventRepository : BaseRepository, IOutboxEventRepository
    {
        public OutboxEventRepository(IConnectionFactory factory) : base(factory, DatabaseKey.Default) { }

        public async Task<IEnumerable<OutboxEvent>> GetAllAsync()
        {
            return await WithConnectionAsync(conn =>
                conn.QueryAsync<OutboxEvent>(" SELECT * FROM OutboxEvents "));
        }
    }
}
