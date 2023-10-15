using Discount.grpc.Entities;
using Discount.grpc.Repositories;
using Discount.grpc.Repositories.Interface;
using Discount.grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// added conn str for db access
var connectionString = builder.Configuration.GetConnectionString("ProductDB");
builder.Services.AddDbContextPool<ProductDBContext>(option =>
option.UseSqlServer(connectionString)
);

// Add services to the container.
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
// added autoapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// grpc
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
