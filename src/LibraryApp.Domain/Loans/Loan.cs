namespace LibraryApp.Domain.Loans;

public class Loan
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid BookId { get; private set; }
    public int BookCopyId { get; private set; }
    public DateOnly LoanDate { get; private set; }
    public DateOnly DueDate { get; private set; }
    public DateOnly? ReturnedDate { get; private set; }
    public bool IsActive { get; private set; }

    private Loan(Guid userId, Guid bookId, int bookCopyId)
    {
        if (userId == Guid.Empty) throw new ArgumentException("UserId cannot be empty.", nameof(userId));
        if (bookId == Guid.Empty) throw new ArgumentException("BookId cannot be empty.", nameof(bookId));
        if (bookCopyId <= 0) throw new ArgumentException("BookCopyId must be a positive integer.", nameof(bookCopyId));

        Id = Guid.NewGuid();
        UserId = userId;
        BookId = bookId;
        BookCopyId = bookCopyId;
        LoanDate = DateOnly.FromDateTime(DateTime.Now);
        DueDate = LoanDate.AddDays(7);
        ReturnedDate = null;
        IsActive = true;
    }

    public static Loan Create(Guid userId, Guid bookId, int bookCopyId)
    {
        return new Loan (userId, bookId, bookCopyId);
    }

    public void Return()
    {
        if (!IsActive)
            throw new InvalidOperationException("Loan is already returned.");

        ReturnedDate = DateOnly.FromDateTime(DateTime.Now);
        IsActive = false;
    }
}
