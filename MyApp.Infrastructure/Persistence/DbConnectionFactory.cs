using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Persistence
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection(DatabaseKey key);
    }

    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _config;

        public DbConnectionFactory(IConfiguration config) => _config = config;

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
