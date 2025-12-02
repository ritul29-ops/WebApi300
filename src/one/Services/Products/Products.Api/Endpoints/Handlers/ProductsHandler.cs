using Marten;
using Products.Api.Endpoints.Events;

namespace Products.Api.Endpoints.Handlers;

// commands "belong with" the handler for that command.
// a command can come from 1+ sources, but is always handled by one bit of code. The handler.

public record CreateProduct(Guid Id, string Name, decimal Price, int Qty);


public class ProductsHandler
{

    public async Task HandleAsync(CreateProduct command, IDocumentSession session)
    {
        var (id, name,price, qty ) = command;
        // starting a stream of events all about the life of this product.
        // When a "thingy" (a stream) is brand new, you use StartStream.

        session.Events.StartStream(id, new ProductCreated(id, name, price, qty) );
        
     //   session.Events.Append(command.CreatedBy, ...);
        
        await session.SaveChangesAsync();
    }
}