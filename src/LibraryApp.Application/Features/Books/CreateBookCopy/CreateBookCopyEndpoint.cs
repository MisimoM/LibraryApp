using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.ResultPattern;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace LibraryApp.Application.Features.Books.CreateBookCopy;

public class CreateBookCopyEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/books/{id}/copies", async (Guid id, CreateBookCopyRequest request, CreateBookCopyHandler handler, CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(bookId: id, request, cancellationToken);

            return result.IsSuccess
                ? Results.Created($"/books/{result.Value.Id}", result.Value)
                : result.Error.ToHttpResult();
        })
        .Produces<CreateBookCopyResponse>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status409Conflict)
        .Produces(StatusCodes.Status400BadRequest)
        .WithName("CreateBookCopy")
        .WithTags("Books");
    }
}
