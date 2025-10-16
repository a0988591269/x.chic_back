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
        private readonly DatabaseKey _dbKey;

        protected BaseRepository(IDbConnectionFactory factory, DatabaseKey dbKey)
        {
            _factory = factory;
            _dbKey = dbKey;
        }

        protected async Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> func)
        {
            using var conn = _factory.CreateConnection(_dbKey);
            return await func(conn);
        }

        protected async Task<int> WithConnectionAsync(Func<IDbConnection, Task<int>> func)
        {
            using var conn = _factory.CreateConnection(_dbKey);
            return await func(conn);
        }
    }
}
