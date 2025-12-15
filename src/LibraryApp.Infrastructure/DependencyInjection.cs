using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace LibraryApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("library-db")));

        return services;
    }

    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IConnectionFactory>(sp =>
        {
            var connectionString = config.GetConnectionString("rabbitmq")!;
            return new ConnectionFactory { Uri = new Uri(connectionString) };
        });

        return services;
    }
}
