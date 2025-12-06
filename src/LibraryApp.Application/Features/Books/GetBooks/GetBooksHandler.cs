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
            Id: b.Id,
            Title: b.Title,
            Author: b.Author,
            Isbn: b.Isbn,
            Publisher: b.Publisher,
            PublicationDate: b.PublicationDate,
            Category: b.Category,
            Language: b.Language,
            Description: b.Description,
            CoverImageUrl: b.CoverImageUrl,
            Copies: b.Copies.Select(c => new GetBookCopyResponse(
                BookCopyId: c.BookCopyId,
                Status: c.Status.ToString()
            ))
        ));
    }
}
