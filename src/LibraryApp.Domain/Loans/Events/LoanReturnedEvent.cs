using LibraryApp.Domain.Common;

namespace LibraryApp.Domain.Loans.Events;

public record LoanReturnedEvent(Guid LoanId, Guid UserId, Guid BookId, Guid BookCopyId) : IDomainEvent;
