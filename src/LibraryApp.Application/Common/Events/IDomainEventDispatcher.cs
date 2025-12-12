using LibraryApp.Domain.Common;

namespace LibraryApp.Application.Common.Events;

public interface IDomainEventDispatcher
{
    Task Dispatch(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken);
}
