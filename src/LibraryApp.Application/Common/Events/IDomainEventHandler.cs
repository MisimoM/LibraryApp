using LibraryApp.Domain.Common;

namespace LibraryApp.Application.Common.Events;

public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
{
    Task Handle(TEvent domainEvent, CancellationToken cancellationToken);
}
