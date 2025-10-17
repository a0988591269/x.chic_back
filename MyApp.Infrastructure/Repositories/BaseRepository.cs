using MyApp.Domain.Enums;
using MyApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        private readonly IDbConnectionFactory _factory;
        private readonly DatabaseKey _db;

        protected BaseRepository(IDbConnectionFactory factory, DatabaseKey db)
        {
            _factory = factory;
            _db = db;
        }

        protected async Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> func)
        {
            using var conn = _factory.CreateConnection(_db);
            return await func(conn);
        }
    }
}
