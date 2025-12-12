using LibraryApp.Application.Common.Events;
using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.ResultPattern;
using LibraryApp.Domain.Loans;

namespace LibraryApp.Application.Features.Loans.ReturnLoan;

public class ReturnLoanHandler
{
    private readonly ILoanRepository _loanRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public ReturnLoanHandler(ILoanRepository loanRepository, IBookRepository bookRepository, IDomainEventDispatcher domainEventDispatcher)
    {
        _loanRepository = loanRepository;
        _bookRepository = bookRepository;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task<Result<ReturnLoanResponse>> Handle(Guid loanId, CancellationToken cancellationToken)
    {
        var loan = await _loanRepository.GetById(loanId, cancellationToken);
        if (loan is null)
            return new NotFoundError("LoanNotFound", $"Loan with id '{loanId}' was not found");

        if (loan.Status == LoanStatus.Returned)
            return new ConflictError("AlreadyReturned", "This loan has already been returned");

        var book = await _bookRepository.GetById(loan.BookId, cancellationToken);
        if (book is null)
            return new UnexpectedError("InconsistentLoanState", $"Loan '{loan.Id}' references a Book with id '{loan.BookId}' that does not exist.");

        var bookCopy = book.GetCopy(loan.BookCopyId);
        if (bookCopy is null)
            return new UnexpectedError("InconsistentLoanState", $"Loan '{loan.Id}' references a BookCopy with id '{loan.BookCopyId}' that does not exist.");

        bookCopy.MarkAsReturned();
        loan.Return();

        await _loanRepository.Update(loan, cancellationToken);
        await _bookRepository.Update(book, cancellationToken);

        await _domainEventDispatcher.Dispatch(loan.DomainEvents, cancellationToken);
        loan.ClearDomainEvents();

        return new ReturnLoanResponse(loan.Id, book.Title, loan.ReturnedDate.GetValueOrDefault(DateTime.UtcNow));
    }
}
