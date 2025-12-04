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
    public class AuditLogRepository : BaseRepository, IAuditLogRepository
    {
        public AuditLogRepository(IConnectionFactory factory) : base(factory, DatabaseKey.Default) { }

        public async Task<IEnumerable<AuditLog>> GetAllAsync()
        {
            return await WithConnectionAsync(conn =>
                conn.QueryAsync<AuditLog>(" SELECT * FROM AuditLogs "));
        }
    }
}
