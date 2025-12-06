using LibraryApp.Domain.Books;

namespace LibraryApp.Application.Features.Books.GetBooks;

public record GetBookCopyResponse(int BookCopyId, string Status);
