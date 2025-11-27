using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.ResultPattern;

namespace LibraryApp.Application.Features.Books.CreateBookCopy;

public class CreateBookCopyHandler
{
    private readonly IBookRepository _bookRepository;

    public CreateBookCopyHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Result<CreateBookCopyResponse>> Handle(Guid bookId, CreateBookCopyRequest request, CancellationToken cancellationToken)
    {
        var validationResult = CreateBookCopyValidator.Validate(request);
        if (validationResult.IsFailure)
            return validationResult.Error;

        var book = await _bookRepository.GetById(bookId, cancellationToken);

        if (book == null)
            return new NotFoundError("BookNotFound", $"A book with id '{bookId}' was not found");

        if (book.Copies.Count + request.Count > 20)
            return new ConflictError(
                "BookCopyConflict",
                $"Cannot create {request.Count} copies. This book already has {book.Copies.Count} copies, max is 20."
            );

        book.AddCopies(request.Count);

        await _bookRepository.Update(book, cancellationToken);

        return new CreateBookCopyResponse(book.Id, book.Title, request.Count);
    }
}
