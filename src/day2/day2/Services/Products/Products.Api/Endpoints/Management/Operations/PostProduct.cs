using System.Security.Claims;
using Marten;
using Microsoft.AspNetCore.Http.HttpResults;
using Products.Api.Endpoints.Management.Handlers;
using Products.Api.Endpoints.Management.ReadModels;
using Wolverine;

namespace Products.Api.Endpoints.Management.Operations;

public static class PostProduct
{
    public static async Task<Ok<ProductDetails>> AddProductAsync(
        Models.ProductCreateRequest request,
        IMessageBus messaging,
        IDocumentSession session
    )
    {
        var command = new CreateProduct(Guid.NewGuid(), request.Name, request.Price, request.Qty);
        await messaging.InvokeAsync(command);
        var response = await session.Events.FetchLatest<ProductDetails>(command.Id)!;
        return TypedResults.Ok(response);
    }
}