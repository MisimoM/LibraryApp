using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Domain.Books;

namespace LibraryApp.Application.Features.Books.CreateBook;

public class CreateBookHandler
{
    private readonly IBookRepository _bookRepository;

    public CreateBookHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async Task<CreateBookResponse> Handle(CreateBookRequest request, CancellationToken cancellationToken)
    {
        var book = Book.Create(
            request.Title,
            request.Author,
            request.Isbn,
            request.Publisher,
            request.PublicationDate,
            request.Category,
            request.Language,
            request.Description,
            request.CoverImageUrl
        );

        await _bookRepository.Add(book, cancellationToken);

        return new CreateBookResponse(book.Id);
    }
}
