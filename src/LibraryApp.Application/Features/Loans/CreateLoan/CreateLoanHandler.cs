using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.ResultPattern;
using LibraryApp.Domain.Books;
using LibraryApp.Domain.Loans;

namespace LibraryApp.Application.Features.Loans.CreateLoan;

public class CreateLoanHandler
{
    private readonly IBookRepository _bookRepository;
    private readonly ILoanRepository _loanRepository;
    private readonly IUserRepository _userRepository;

    public CreateLoanHandler(
        IBookRepository bookRepository,
        ILoanRepository loanRepository,
        IUserRepository userRepository)
    {
        _bookRepository = bookRepository;
        _loanRepository = loanRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<CreateLoanResponse>> Handle(CreateLoanRequest request, CancellationToken cancellationToken)
    {
        var validationResult = CreateLoanValidator.Validate(request);
        if (validationResult.IsFailure)
            return validationResult.Error;

        var user = await _userRepository.GetById(request.UserId, cancellationToken);
        if (user is null)
            return new NotFoundError("UserNotFound", $"User with id '{request.UserId}' was not found");

        var book = await _bookRepository.GetById(request.BookId, cancellationToken);
        if (book is null)
            return new NotFoundError("BookNotFound", $"Book with id '{request.BookId}' was not found");

        var bookCopy = book.GetCopy(request.BookCopyId);
        if (bookCopy is null)
            return new NotFoundError("BookCopyNotFound", $"Book copy with id '{request.BookCopyId}' was not found");

        if (bookCopy.Status is not BookCopyStatus.Available)
            return new ConflictError(
                "BookCopyConflict",
                $"Book copy with id '{request.BookCopyId}' is not available"
            );

        var loan = Loan.Create(request.UserId, request.BookId, request.BookCopyId);

        bookCopy.MarkAsBorrowed();
        book.IncrementBorrowCount();

        await _loanRepository.Add(loan, cancellationToken);
        await _bookRepository.Update(book, cancellationToken);

        return new CreateLoanResponse(loan.Id, user.Id, book.Id, bookCopy.Id, book.Title, loan.DueDate);
    }

}
