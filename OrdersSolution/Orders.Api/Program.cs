using Orders.Api.Endpoints.Orders;
using Orders.Api.Endpoints.Orders.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOrders();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build(); // one world above the build, one after.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapOrders(); // This will add all the operations for the "/orders" resource.
 
app.Run();
