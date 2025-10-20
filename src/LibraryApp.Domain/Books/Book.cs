namespace LibraryApp.Domain.Books;

public class Book
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string Isbn { get; private set; }
    public string Publisher { get; private set; }
    public DateOnly PublicationDate { get; private set; }
    public BookCategory Category { get; private set; }
    public BookLanguage Language { get; private set; }
    public string? Description { get; private set; }
    public string? CoverImageUrl { get; private set; }
    public int BorrowCount { get; private set; }
    
    private readonly List<BookCopy> _copies = new();
    public IReadOnlyCollection<BookCopy> Copies => _copies.AsReadOnly();


    private Book(
        string title,
        string author,
        string isbn,
        string publisher,
        DateOnly publicationDate,
        BookCategory category,
        BookLanguage language,
        string? description,
        string? coverImageUrl)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Author cannot be empty.", nameof(author));
        if (string.IsNullOrWhiteSpace(isbn))
            throw new ArgumentException("ISBN cannot be empty.", nameof(isbn));
        if (string.IsNullOrWhiteSpace(publisher))
            throw new ArgumentException("Author cannot be empty.", nameof(publisher));
        if (publicationDate > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException("Publication date cannot be in the future.", nameof(publicationDate));
        if (!Enum.IsDefined(category))
            throw new ArgumentException("Invalid book category.", nameof(category));
        if (!Enum.IsDefined(language))
            throw new ArgumentException("Invalid book language.", nameof(language));

        Id = Guid.NewGuid();
        Title = title;
        Author = author;
        Isbn = isbn;
        Publisher = publisher;
        PublicationDate = publicationDate;
        Category = category;
        Language = language;
        Description = description;
        CoverImageUrl = coverImageUrl;
        BorrowCount = 0;
    }

    public static Book Create(
        string title,
        string author,
        string isbn,
        string publisher,
        DateOnly publicationDate,
        BookCategory category,
        BookLanguage language,
        string? description,
        string? coverImageUrl)
    {
        return new Book(title, author, isbn, publisher, publicationDate, category, language, description, coverImageUrl);
    }

    public void Update(
        string title,
        string author,
        string isbn,
        string publisher,
        DateOnly publicationDate,
        BookCategory category,
        BookLanguage language,
        string? description,
        string? coverImageUrl)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Author cannot be empty.", nameof(author));
        if (string.IsNullOrWhiteSpace(isbn))
            throw new ArgumentException("ISBN cannot be empty.", nameof(isbn));
        if (string.IsNullOrWhiteSpace(publisher))
            throw new ArgumentException("Author cannot be empty.", nameof(publisher));
        if (publicationDate > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException("Publication date cannot be in the future.", nameof(publicationDate));
        if (!Enum.IsDefined(category))
            throw new ArgumentException("Invalid book category.", nameof(category));
        if (!Enum.IsDefined(language))
            throw new ArgumentException("Invalid book language.", nameof(language));

        Title = title;
        Author = author;
        Isbn = isbn;
        Publisher = publisher;
        PublicationDate = publicationDate;
        Category = category;
        Language = language;
        Description = description;
        CoverImageUrl = coverImageUrl;
    }

    public BookCopy AddCopy()
    {
        int bookCopyId = _copies.Count + 1;
        var copy = new BookCopy(Id, bookCopyId);
        _copies.Add(copy);
        return copy;
    }

    internal void IncrementBorrowCount() => BorrowCount++;
}
