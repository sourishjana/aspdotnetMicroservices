using Basket.API.Repositories;
using Basket.API.Repositories.Interface;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// General Configuration
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddSingleton<IConnectionMultiplexer, ConnectionMultiplexer>(c => {
    return ConnectionMultiplexer.Connect("localhost");
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
