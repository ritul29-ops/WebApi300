using Microsoft.AspNetCore.Http.HttpResults;
using Orders.Api.Endpoints.Orders.Services;

namespace Orders.Api.Endpoints.Orders.Operation;

public record ProcessOrder(Guid Id, ShoppingCartRequest Cart);
public static class Post
{
    public static async Task<Ok<Order>> AddOrderAsync(ShoppingCartRequest request,  CancellationToken token)
    {
        // Todo: Validate - after lunch.
        // what about all the work we have to do??

        // schedule this work, get back to it later. 

        var orderId = Guid.NewGuid();

        // hey, some other thing, get to work on this. WTCYWYK

        // write this to a database table, have a background worker get to it later..
        // But we will have to make this durable. 
       // await _messageBus.HandleAsync(new ProcessOrder(orderId, shoppingCart, cancellationToken));
        // I want this to happen, but I don't want the problem we had before (it throws and exception and nobody handles it, etc.)
        // so write it to a "Durable store' - like a database, and if THAT succeeds, then have the background worker get to it
        // whenever.
        // in .NET the "Mediatr" library - Jimmy Bogard. Now commercial. 
        // Mediator pattern is close to what we are doing here. 
        // .NET Channels (cool feature) can do this, but it isn't durable.


        var order = new Order
        {
            Id = orderId,
            Total = request.Amount * 1.13M
        };
        return TypedResults.Ok(order); // the caller only gets this.
    }
}

