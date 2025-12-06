using LibraryApp.Domain.Books;

namespace LibraryApp.Application.Features.Books.GetBooks;

public record GetBooksResponse(
    Guid Id,
    string Title,
    string Author,
    string Isbn,
    string Publisher,
    DateOnly PublicationDate,
    BookCategory Category,
    BookLanguage Language,
    string? Description,
    string? CoverImageUrl,
    IEnumerable<GetBookCopyResponse> Copies
);