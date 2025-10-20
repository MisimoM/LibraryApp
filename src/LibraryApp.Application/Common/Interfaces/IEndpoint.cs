using Microsoft.AspNetCore.Routing;

namespace LibraryApp.Application.Common.Interfaces;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder builder);
}
