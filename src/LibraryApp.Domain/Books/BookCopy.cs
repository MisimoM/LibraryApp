namespace LibraryApp.Domain.Books;

public class BookCopy
{
    public Guid Id { get; private set; }
    public Guid BookId { get; private set; }
    public int BookCopyId { get; private set; }
    public BookCopyStatus Status { get; private set; }
    public DateTime? LastBorrowedAt { get; private set; }

    internal BookCopy(Guid bookId, int bookCopyId)
    {
        BookId = bookId;
        BookCopyId = bookCopyId;
        Status = BookCopyStatus.Available;
        LastBorrowedAt = null;
    }

    public void MarkAsBorrowed()
    {
        if (Status is not BookCopyStatus.Available)
            throw new InvalidOperationException("Book copy is not available for borrowing.");

        Status = BookCopyStatus.Borrowed;
        LastBorrowedAt = DateTime.UtcNow;
    }

    public void MarkAsReturned()
    {
        if (Status is not BookCopyStatus.Borrowed)
            throw new InvalidOperationException("Book copy is not borrowed.");

        Status = BookCopyStatus.Available;
    }
}
