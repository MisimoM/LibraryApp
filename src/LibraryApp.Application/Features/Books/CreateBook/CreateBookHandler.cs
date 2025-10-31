using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.ResultPattern;
using LibraryApp.Domain.Books;

namespace LibraryApp.Application.Features.Books.CreateBook;

public class CreateBookHandler
{
    private readonly IBookRepository _bookRepository;

    public CreateBookHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async Task<Result<Book>> Handle(CreateBookRequest request, CancellationToken cancellationToken)
    {
        var validationResult = CreateBookValidator.Validate(request);
        if (validationResult.IsFailure)
            return validationResult.Error;

        if (await _bookRepository.Exists(request.Title, cancellationToken))
            return new ConflictError("BookConflict", $"A book with title '{request.Title}' already exists");

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

        return book;
    }
}
