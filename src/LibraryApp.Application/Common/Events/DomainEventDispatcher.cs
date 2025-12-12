using LibraryApp.Domain.Common;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApp.Application.Common.Events;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Dispatch(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        foreach (var domainEvent in domainEvents)
        {
            var handlerType = typeof(IDomainEventHandler<>)
                .MakeGenericType(domainEvent.GetType());

            var handlers = (IEnumerable<object>)_serviceProvider.GetServices(handlerType);

            foreach (dynamic handler in handlers)
            {
                await handler.Handle((dynamic)domainEvent, cancellationToken);
            }
        }
    }
}
