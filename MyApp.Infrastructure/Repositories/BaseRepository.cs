using MyApp.Domain.Enums;
using MyApp.Infrastructure.Persistence.Contexts;
using System.Data;

namespace MyApp.Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        private readonly IDapperConnectionFactory _factory;
        private readonly DatabaseKey _db;

        protected BaseRepository(IDapperConnectionFactory factory, DatabaseKey db)
        {
            _factory = factory;
            _db = db;
        }

        protected async Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> func)
        {
            using var conn = _factory.CreateConnection(_db);
            return await func(conn);
        }

        protected async Task WithConnectionAsync(Func<IDbConnection, Task> action)
        {
            using var conn = _factory.CreateConnection(_db);
            await action(conn);
        }

        protected async Task<T> WithTransactionAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> func)
        {
            using var conn = _factory.CreateConnection(_db);
            using var tran = conn.BeginTransaction();
            try
            {
                var result = await func(conn, tran);
                tran.Commit();
                return result;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }
    }
}
