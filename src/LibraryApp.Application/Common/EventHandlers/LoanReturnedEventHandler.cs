using LibraryApp.Application.Common.Events;
using LibraryApp.Domain.Loans.Events;

namespace LibraryApp.Application.Common.EventHandlers;

public class LoanReturnedEventHandler : IDomainEventHandler<LoanReturnedEvent>
{
    public Task Handle(LoanReturnedEvent domainEvent, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Loan returned: {domainEvent.LoanId}");
        return Task.CompletedTask;
    }
}