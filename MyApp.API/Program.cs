using MyApp.API.Extensions;
using MyApp.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region 自定義服務
builder.Services.ConfigureCors();
// 單一資料庫
builder.Services.AddContext(builder.Configuration);
builder.Services.AddMultipleContext()
#endregion

var app = builder.Build();

#region 自定義運行
app.UseCors("AllowOrigin");
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
