using LibraryApp.Application.Common.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace LibraryApp.Application.Features.Loans.GetLoans;

public class GetLoansEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/loans", async(GetLoansHandler handler, CancellationToken cancellationToken) =>
        {
            var loans = await handler.Handle(cancellationToken);
            return Results.Ok(loans);
        })
        .Produces<IEnumerable<GetLoansResponse>>(StatusCodes.Status200OK)
        .WithName("GetLoans")
        .WithTags("Loans");
    }
}
