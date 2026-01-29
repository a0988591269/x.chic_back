using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MyApp.API.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Enable CORS，阻止來自不同網域的請求，Configure需加入
        /// </summary>
        public static void ConfigureCors(this IServiceCollection services)
        {
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowOrigin", policy =>
            //    {
            //        policy.AllowAnyOrigin()
            //            .AllowAnyHeader()
            //            .AllowAnyMethod();
            //    });
            //});

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("NuxtApp", policy =>
            //    {
            //        policy.WithOrigins("http://localhost:3000") // ❌ 絕對不能寫 "*"
            //              .AllowAnyHeader()
            //              .AllowAnyMethod()
            //              .AllowCredentials();                  // 🔥 關鍵：允許帶 Cookie
            //    });
            //});
        }

        /// <summary>
        /// 設定 JWT 認證服務
        /// </summary>
        public static void AddAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var jwtSection = config.GetSection("Jwt");
            var jwtSecret = jwtSection["Secret"];
            var jwtIssuer = jwtSection["Issuer"];
            var jwtAudience = jwtSection["Audience"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSecret ?? ""))
                };

                // ⭐ 從 Cookie 讀 token
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["access_token"];
                        return Task.CompletedTask;
                    }
                };
            });
        }

        /// <summary>
        /// 配置靜態文件中介軟體以提供圖片
        /// </summary>
        public static void UseStaticFilesExtensions(this IApplicationBuilder builder)
        {
            var imageRoot = @"C:\Temp"; // ← 這是你的圖片存放路徑
            if (!Directory.Exists(imageRoot))
            {
                Directory.CreateDirectory(imageRoot);
            }
            builder.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(imageRoot),
                RequestPath = "/images"
            });
        }
    }
}
