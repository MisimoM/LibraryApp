using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Features.Books.CreateBook;
using LibraryApp.Application.Features.Books.CreateBookCopy;
using LibraryApp.Application.Features.Books.GetBooks;
using LibraryApp.Application.Features.Loans.CreateLoan;
using LibraryApp.Application.Features.Loans.ReturnLoan;
using LibraryApp.Application.Features.Users.CreateUser;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateBookHandler>();
        services.AddScoped<GetBooksHandler>();
        services.AddScoped<CreateBookCopyHandler>();
        
        services.AddScoped<CreateUserHandler>();
        
        services.AddScoped<CreateLoanHandler>();
        services.AddScoped<ReturnLoanHandler>();

        return services;
    }
    public static WebApplication MapEndpoints(this WebApplication app)
    {

        var endpointTypes = typeof(DependencyInjection).Assembly.GetTypes()
            .Where(t => typeof(IEndpoint).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var endpointType in endpointTypes)
        {
            var endpoint = (IEndpoint)Activator.CreateInstance(endpointType)!;
            endpoint.MapEndpoint(app);
        }

        return app;
    }
}
