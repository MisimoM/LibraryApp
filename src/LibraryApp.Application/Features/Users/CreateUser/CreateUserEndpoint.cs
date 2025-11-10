using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.ResultPattern;
using LibraryApp.Domain.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace LibraryApp.Application.Features.Users.CreateUser;

public class CreateUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/users", async (CreateUserRequest request, CreateUserHandler handler, CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(request, cancellationToken);

            return result.IsSuccess
               ? Results.Created($"/users/{result.Value.Id}", result.Value)
               : result.Error.ToHttpResult();
        })
        .Produces<CreateUserResponse>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status409Conflict)
        .Produces(StatusCodes.Status400BadRequest)
        .WithName("CreateUser")
        .WithTags("Users");
    }
}
