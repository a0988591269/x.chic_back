using Microsoft.AspNetCore.Mvc.ApiExplorer;
using MyApp.Infrastructure.Persistence.Contexts;
using MyApp.Infrastructure.Persistence.Seed;

namespace MyApp.API.Extensions
{
    public static class SwaggerExtensions
    {
        /// <summary>
        /// 啟用 Swagger UI 中介軟體
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerUI_v1(this IApplicationBuilder app)
        {
            return app.UseSwaggerUI(c =>
            {
                //c.ConfigObject.TryItOutEnabled = true;
                //c.ConfigObject.DisplayRequestDuration = true;
                //c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/swagger/Category/swagger.json", "Category API");
                c.SwaggerEndpoint("/swagger/Product/swagger.json", "Product API");
            });
        }

        /// <summary>
        /// 註冊多個 Swagger 文件
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggerUI_v1(this IServiceCollection services)
        {
            // 手動註冊多個 Swagger 文件
            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("Category", new() { Title = "Category API", Version = "v1" });
            //    options.SwaggerDoc("Products", new() { Title = "Product API", Version = "v1" });

            //    // 依據 Controller 上的 GroupName 分配 endpoints
            //    options.DocInclusionPredicate((docName, apiDesc) =>
            //    {
            //        var group = apiDesc.GroupName;
            //        return group != null && group == docName;
            //    });
            //});

            // 自動註冊多個 Swagger 文件
            services.AddSwaggerGen(options =>
            {
                // 自動建立 group (不需要手動新增 SwaggerDoc)
                var descriptions = services.BuildServiceProvider()
                    .GetRequiredService<IApiDescriptionGroupCollectionProvider>()
                    .ApiDescriptionGroups.Items;

                foreach (var desc in descriptions)
                {
                    if (!string.IsNullOrEmpty(desc.GroupName))
                    {
                        options.SwaggerDoc(desc.GroupName, new()
                        {
                            Title = $"{desc.GroupName} API",
                            Version = "v1"
                        });
                    }
                }

                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var group = apiDesc.GroupName;
                    return group != null && group == docName;
                });
            });

        }
    }
}
