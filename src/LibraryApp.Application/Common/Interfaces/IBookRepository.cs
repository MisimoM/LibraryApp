using LibraryApp.Domain.Books;

namespace LibraryApp.Application.Common.Interfaces
{
    public interface IBookRepository
    {
        Task Add(Book book, CancellationToken cancellationToken);
    }
}
