using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using MyApp.API.Extensions;
using MyApp.Application.DependencyInjection;
using MyApp.Infrastructure.DependencyInjection;
using MyApp.Infrastructure.Persistence.Seed.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region 自定義服務
builder.Services.ConfigureCors();
// 單一資料庫
//builder.Services.AddDapperSingle(builder.Configuration);
// 註冊 Application 層 DI
builder.Services.AddApplicationServices(builder.Configuration);
// 註冊 Infrastructure 層 DI
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwaggerUIExtensions();
#endregion

var app = builder.Build();

#region 自定義運行
app.UseCors("AllowOrigin");
await app.SeedDataAsync();
app.UseSwaggerUIExtensions();
#endregion

#region 自定義中介軟體
// 配置靜態文件中介軟體以提供圖片
var imageRoot = @"C:\Temp"; // ← 這是你的圖片存放路徑
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imageRoot),
    RequestPath = "/images"
});
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
