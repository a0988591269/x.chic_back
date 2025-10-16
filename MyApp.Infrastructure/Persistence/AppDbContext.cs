using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyApp.Infrastructure.Persistence
{
    public static class AppDbContext
    {
        public static IServiceCollection AddDefaultConn(this IServiceCollection services, IConfiguration config)
        {
            //Dapper 是「輕量、無 context 的 ORM」，所以建議使用 Transient 是最安全的選擇
            // _ 表示未使用參數。
            services.AddTransient<IDbConnection>(_ =>
            {
                //檢查 connection string 是否為 null。
                var connString = config.GetConnectionString("DefaultConnection")
                                 ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                //明確 return 新的連線物件。
                return new SqlConnection(connString);
            });

            return services;
        }
    }
}
