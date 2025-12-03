using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.ResultPattern;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace LibraryApp.Application.Features.Loans.CreateLoan;

public class CreateLoanEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/loans", async (CreateLoanRequest request, CreateLoanHandler handler, CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(request, cancellationToken);

            return result.IsSuccess
                ? Results.Created($"/loans/{result.Value.LoanId}", result.Value)
                : result.Error.ToHttpResult();
        })
        .Produces<CreateLoanResponse>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status409Conflict)
        .Produces(StatusCodes.Status400BadRequest)
        .WithName("CreateLoan")
        .WithTags("Loans");
    }
}
