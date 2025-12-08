using LibraryApp.Domain.Books;

namespace LibraryApp.Application.Features.Books.GetBooks;

public record GetBookCopyResponse(Guid Id, int BookCopyId, string Status);
