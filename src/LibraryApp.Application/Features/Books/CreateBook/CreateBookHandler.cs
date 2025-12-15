using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.ResultPattern;
using LibraryApp.Domain.Books;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.Features.Books.CreateBook;

public class CreateBookHandler
{
    private readonly IApplicationDbContext _dbContext;
    public CreateBookHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<Book>> Handle(CreateBookRequest request, CancellationToken cancellationToken)
    {
        var validationResult = CreateBookValidator.Validate(request);
        if (validationResult.IsFailure)
            return validationResult.Error;

        if (await _dbContext.Books.AnyAsync(b => b.Title == request.Title, cancellationToken))
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

        _dbContext.Books.Add(book);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return book;
    }
}
