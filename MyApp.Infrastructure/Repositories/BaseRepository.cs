using MyApp.Domain.Enums;
using MyApp.Infrastructure.Persistence.Contexts;
using System.Data;

namespace MyApp.Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        // private = 只有 同一個 class 裡面能用，子類別不能用
        private readonly IDapperConnectionFactory _factory;
        private readonly DatabaseKey _db;

        // protected = 只有 同一個 assembly 裡面能用，子類別(繼承)能用
        protected BaseRepository(IDapperConnectionFactory factory, DatabaseKey db)
        {
            _factory = factory;
            _db = db;
        }

        /// <summary>
        /// 單 SQL，無需回傳值
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        protected async Task WithConnectionAsync(Func<IDbConnection, Task> func)
        {
            using var conn = _factory.CreateConnection(_db);
            await func(conn);
        }

        /// <summary>
        /// 單 SQL，需回傳值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="func">函式：傳入一個 IDbConnection，回傳一個 Task<T> </param>
        /// <returns>Task<T></returns>
        protected async Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> func)
        {
            using var conn = _factory.CreateConnection(_db);
            return await func(conn);
        }

        /// <summary>
        /// 多 SQL，無需回傳值
        /// </summary>
        protected async Task WithTransactionAsync(Func<IDbConnection, IDbTransaction, Task> func)
        {
            using var conn = _factory.CreateConnection(_db);
            using var tran = conn.BeginTransaction();
            try
            {
                await func(conn, tran);
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 多 SQL，需回傳值
        /// </summary>
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
