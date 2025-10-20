using LibraryApp.Application.Common.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace LibraryApp.Application.Features.Books.CreateBook;

public class CreateBookEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/books", async (CreateBookRequest request, CreateBookHandler handler, CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(request, cancellationToken);

            return Results.Created($"/books/{response.Id}", response);
        })
        .Produces<CreateBookResponse>(StatusCodes.Status201Created);
    }
}
