using StoreApp.Core.Services;
using StoreApp.Core.Repositories;
using StoreApp.Core.Contracts;
using MongoDB.Driver;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMongoClient, MongoClient>(_ => new MongoClient("mongodb+srv://dovism:SLAPTAZODIS@cluster0.dh7gm.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"));
builder.Services.AddTransient<IMongoCacheRepository, MongoCacheRepository>();
builder.Services.AddSingleton<ICacheService, CacheService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>(_ => new ProductRepository());
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IUserRepository, UserRepository>(_ => new UserRepository());
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>(_ => new OrderRepository());
builder.Services.AddTransient<IOrderService, OrderService>();


var log = new LoggerConfiguration()
    .WriteTo.File("C:\\Users\\dovis\\source\\repos\\StoreApp.Core\\StoreApp.Core\\Logs\\LOGS.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

Log.Logger = log;

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("https://localhost:7197")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var cacheService = app.Services.GetRequiredService<ICacheService>();
cacheService.DropCaches();
Log.Information("Application started");
app.Run();
