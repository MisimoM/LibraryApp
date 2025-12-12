using LibraryApp.Domain.Books;
using LibraryApp.Domain.Common;
using LibraryApp.Domain.Loans.Events;
using LibraryApp.Domain.Users;

namespace LibraryApp.Domain.Loans;

public class Loan : Entity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid BookId { get; private set; }
    public Guid BookCopyId { get; private set; }
    public DateTime LoanDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? ReturnedDate { get; private set; }
    public LoanStatus Status { get; private set; }

    public Book Book { get; private set; } = null!;
    public User User { get; private set; } = null!;

    private Loan(Guid userId, Guid bookId, Guid bookCopyId)
    {
        if (userId == Guid.Empty) throw new ArgumentException("UserId cannot be empty.", nameof(userId));
        if (bookId == Guid.Empty) throw new ArgumentException("BookId cannot be empty.", nameof(bookId));
        if (bookCopyId == Guid.Empty) throw new ArgumentException("BookCopyId cannot be empty.", nameof(bookCopyId));

        Id = Guid.NewGuid();
        UserId = userId;
        BookId = bookId;
        BookCopyId = bookCopyId;
        LoanDate = DateTime.UtcNow;
        DueDate = LoanDate.AddDays(7);
        ReturnedDate = null;
        Status = LoanStatus.Loaned;
    }

    public static Loan Create(Guid userId, Guid bookId, Guid bookCopyId)
    {
        return new Loan (userId, bookId, bookCopyId);
    }

    public void Return()
    {
        if (Status != LoanStatus.Loaned)
            throw new InvalidOperationException("Loan is already returned or cannot be returned.");

        ReturnedDate = DateTime.UtcNow;
        Status = LoanStatus.Returned;

        AddDomainEvent(new LoanReturnedEvent(Id, UserId, BookId, BookCopyId));
    }
}
