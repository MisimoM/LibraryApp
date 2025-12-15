using LibraryApp.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.Features.Books.GetBooks;

public class GetBooksHandler
{
    private readonly IApplicationDbContext _dbContext;

    public GetBooksHandler(IApplicationDbContext dbContext)
    {  
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GetBooksResponse>> Handle(CancellationToken cancellationToken)
    {

        return await _dbContext.Books
            .AsNoTracking()
            .Select(b => new GetBooksResponse(
                b.Id,
                b.Title,
                b.Author,
                b.Isbn,
                b.Publisher,
                b.PublicationDate,
                b.Category,
                b.Language,
                b.Description,
                b.CoverImageUrl,
                b.Copies.Select(c => new GetBookCopyResponse(
                    c.Id,
                    c.BookCopyId,
                    c.Status.ToString()
                ))
            )).ToListAsync(cancellationToken);
    }
}
