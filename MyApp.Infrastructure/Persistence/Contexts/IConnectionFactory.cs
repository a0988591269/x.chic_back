using System.Data;

namespace MyApp.Infrastructure.Persistence.Contexts
{
    /// <summary>
    /// 介面
    /// </summary>
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
        IDbConnection GetConnection_Test();
    }
}
