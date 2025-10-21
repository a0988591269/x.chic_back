using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyApp.Domain.Enums;
using System.Data;

namespace MyApp.Infrastructure.Persistence.Contexts
{
    /// <summary>
    /// 介面
    /// </summary>
    public interface IDapperConnectionFactory
    {
        IDbConnection CreateConnection(DatabaseKey key);
    }

    /// <summary>
    /// 實作
    /// </summary>
    public class DapperConnectionFactory : IDapperConnectionFactory
    {
        private readonly IConfiguration _config;

        public DapperConnectionFactory(IConfiguration config) => _config = config;

        public IDbConnection CreateConnection(DatabaseKey key)
        {
            return key switch
            {
                DatabaseKey.Default => new SqlConnection(
                    _config.GetConnectionString("DefaultConnection")),
                DatabaseKey.Test => new SqlConnection(
                    _config.GetConnectionString("TestConnection")),
                //DatabaseType.MySql => new MySqlConnection(
                //    _config.GetConnectionString("MySqlConnection")),
                _ => throw new ArgumentOutOfRangeException($"Connection string '{nameof(key)}' not found.")
            };
        }
    }
}
