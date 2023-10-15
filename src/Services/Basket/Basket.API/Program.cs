using Basket.API.grpcServices;
using Basket.API.Repositories;
using Basket.API.Repositories.Interface;
using Discount.Grpc.Protos;
using MassTransit;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// General Configuration
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddSingleton<IConnectionMultiplexer, ConnectionMultiplexer>(c => {
    return ConnectionMultiplexer.Connect("localhost");
});

// automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Grpc Configuration
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
            (o => o.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]));
builder.Services.AddScoped<DiscountGrpcService>();

// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config => {
    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
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
