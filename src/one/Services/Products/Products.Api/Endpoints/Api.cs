

using Products.Api.Endpoints.Operations;

namespace Products.Api.Endpoints;
public static class ApiExtensions
{

    extension(IServiceCollection services)
    {
        public IServiceCollection AddOrders()
        {
         
            return services;
        }
    }
    extension(IEndpointRouteBuilder builder)
    {
        public IEndpointRouteBuilder MapProductRoutes()
        {

            var group = builder.MapGroup("/products");
            group.MapPost("/", PostProduct.AddProductToInventoryAsync);
            return builder;
        }
    }
}
