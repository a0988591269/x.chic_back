using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyApp.Domain.Contexts;
using System.Data;

namespace MyApp.Infrastructure.Persistence.Contexts
{
    /// <summary>
    /// 實作
    /// </summary>
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _config;

        public ConnectionFactory(IConfiguration config) => _config = config;

        public IDbConnection GetConnection()
        {
            var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return conn;
        }

        public IDbConnection GetConnection_Test()
        {
            var conn = new SqlConnection(_config.GetConnectionString("Test"));
            return conn;
        }
    }
}
