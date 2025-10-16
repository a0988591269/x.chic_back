using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;
using MyApp.Infrastructure.Persistence;
using System.Data;

namespace MyApp.API.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Enable CORS，阻止來自不同網域的請求，Configure需加入
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(builder =>
                builder.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader())
            );
        }
    }
}
