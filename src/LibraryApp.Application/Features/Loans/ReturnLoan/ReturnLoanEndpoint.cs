using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.ResultPattern;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace LibraryApp.Application.Features.Loans.ReturnLoan;

public class ReturnLoanEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost("loans/{id}/return", async (Guid id, ReturnLoanHandler handler, CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(id, cancellationToken);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : result.Error.ToHttpResult();
        })
        .Produces<ReturnLoanResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithName("ReturnLoan")
        .WithTags("Loans");
    }
}
