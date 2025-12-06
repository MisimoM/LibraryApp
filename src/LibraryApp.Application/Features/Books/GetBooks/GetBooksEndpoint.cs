using LibraryApp.Application.Common.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace LibraryApp.Application.Features.Books.GetBooks;

public class GetBooksEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/books", async (GetBooksHandler handler, CancellationToken cancellationToken) =>
        {
            var books = await handler.Handle(cancellationToken);
            return Results.Ok(books);
        })
        .Produces<IEnumerable<GetBooksResponse>>(StatusCodes.Status200OK)
        .WithName("GetBooks")
        .WithTags("Books");
    }
}
