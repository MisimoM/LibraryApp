using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.ResultPattern;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.Features.Books.CreateBookCopy;

public class CreateBookCopyHandler
{
    private readonly IApplicationDbContext _dbContext;

    public CreateBookCopyHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<CreateBookCopyResponse>> Handle(Guid bookId, CreateBookCopyRequest request, CancellationToken cancellationToken)
    {
        var validationResult = CreateBookCopyValidator.Validate(request);
        if (validationResult.IsFailure)
            return validationResult.Error;

        var book = await _dbContext.Books
            .Include(b => b.Copies)
            .FirstOrDefaultAsync(b => b.Id == bookId, cancellationToken);

        if (book == null)
            return new NotFoundError("BookNotFound", $"A book with id '{bookId}' was not found");

        if (book.Copies.Count + request.Count > 20)
            return new ConflictError(
                "BookCopyConflict",
                $"Cannot create {request.Count} copies. This book already has {book.Copies.Count} copies, max is 20."
            );

        book.AddCopies(request.Count);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateBookCopyResponse(book.Id, book.Title, request.Count);
    }
}
