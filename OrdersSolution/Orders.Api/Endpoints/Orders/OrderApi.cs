using Orders.Api.Endpoints.Orders.Operation;
using Orders.Api.Endpoints.Orders.Services;

using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Orders.Api.Endpoints.Orders;

public static class OrderApi
{

    extension(IServiceCollection services)
    {
        public IServiceCollection AddOrders()
        {
            services.AddScoped<CardProcessor>();
            return services;
        }
    }
    extension(IEndpointRouteBuilder builder)
    {
        public IEndpointRouteBuilder MapOrders()
        {
            // I know this will all be stuff for orders, right? so METHOD /orders/ 
            var ordersGroup = builder.MapGroup("/orders");

            ordersGroup.MapGet("/", () => "Your Orders");

            ordersGroup.MapPost("/", Post.AddOrderAsync);

            return builder;
        }
    }
}

public record ShoppingCartRequest
{
    public decimal Amount { get; set; }

    [Required, MinLength(3), MaxLength(100)]
    public string CustomerName { get; set; } = string.Empty;
}

public record Order
{
    public Guid Id { get; set; }
    public decimal Total { get; set; }
}