using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.ResultPattern;
using LibraryApp.Domain.Books;
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
            var result = await handler.Handle(request, cancellationToken);

            return result.IsSuccess
               ? Results.Created($"/books/{result.Value.Id}", result.Value)
               : result.Error.ToHttpResult();
        })
        .Produces<Book>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status409Conflict)
        .Produces(StatusCodes.Status400BadRequest)
        .WithName("CreateBook")
        .WithTags("Books");
    }
}
