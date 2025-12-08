using LibraryApp.Application.Common.Interfaces;

namespace LibraryApp.Application.Features.Books.GetBooks;

public class GetBooksHandler
{
    private readonly IBookRepository _bookRepository;

    public GetBooksHandler(IBookRepository bookRepository)
    {  
        _bookRepository = bookRepository; 
    }

    public async Task<IEnumerable<GetBooksResponse>> Handle(CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAll(cancellationToken);

        return books.Select(b => new GetBooksResponse(
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
        ));
    }
}
