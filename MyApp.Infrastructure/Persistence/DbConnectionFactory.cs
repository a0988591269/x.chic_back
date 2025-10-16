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
        IDbConnection CreateConnection(DatabaseKey dbKey);
    }

    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _config;

        public DbConnectionFactory(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection CreateConnection(DatabaseKey type)
        {
            return type switch
            {
                DatabaseKey.Default =>
                    new SqlConnection(_config.GetConnectionString("DefaultConnection")),
                DatabaseKey.Test =>
                    new SqlConnection(_config.GetConnectionString("TestConnection")),
                //DatabaseKey.Test =>
                //    new MySqlConnection(_config.GetConnectionString("TestConnection")),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}
