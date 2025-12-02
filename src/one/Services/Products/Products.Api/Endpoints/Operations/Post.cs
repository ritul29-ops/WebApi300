using Facet;
using Microsoft.AspNetCore.Http.HttpResults;
using Products.Api.Endpoints.Handlers;
using Wolverine;

namespace Products.Api.Endpoints.Operations;

// mapping is going from a -> b
// get a postcreaterequest -> command -> postcreateresponse



// ProductCreateRequest has to be used to create the CreateProduct command.
[Facet(typeof(CreateProduct), exclude: ["Id"])]
public partial record ProductCreateRequest;



[Facet(typeof(CreateProduct))]
public partial record ProductCreateResponse
{ 
    public string Status => "Pending";
}


public static class PostProduct
{
    public static async Task<Ok<ProductCreateResponse>> AddProductToInventoryAsync(
        ProductCreateRequest request,
        IMessageBus messaging
        )
    {
        var command = new CreateProduct(Guid.NewGuid(), request.Name, request.Price, request.Qty);
        await messaging.PublishAsync( command );
        return TypedResults.Ok(new ProductCreateResponse(command));
    }
}
