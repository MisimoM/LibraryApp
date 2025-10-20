using LibraryApp.Domain.Books;

namespace LibraryApp.Application.Features.Books.CreateBook;

public sealed record CreateBookRequest(
    string Title,
    string Author,
    string Isbn,
    string Publisher,
    DateOnly PublicationDate,
    BookCategory Category,
    BookLanguage Language,
    string? Description,
    string? CoverImageUrl
);
