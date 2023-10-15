using MassTransit;
using Microsoft.EntityFrameworkCore;
using Ordering.API.Entities;
using Ordering.API.EventBusConsumer;
using Ordering.API.Repositories;
using Ordering.API.Repositories.Interface;
using RabbitMqEventBus.Messages.Common;

var builder = WebApplication.CreateBuilder(args);

// added conn str for db
// added conn str for db access
var connectionString = builder.Configuration.GetConnectionString("ProductDB");
builder.Services.AddDbContextPool<ProductDBContext>(option =>
    option.UseSqlServer(connectionString)
);

// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config => {

    config.AddConsumer<BasketCheckoutConsumer>();

    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

        cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c => {
            c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
        });
    });
});
builder.Services.AddScoped<BasketCheckoutConsumer>();

// repo pattern
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
