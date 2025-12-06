using LibraryApp.Domain.Books;

namespace LibraryApp.Application.Common.Interfaces;

public interface IBookRepository
{
    Task Add(Book book, CancellationToken cancellationToken);
    Task<bool> Exists(string title, CancellationToken cancellationToken);
    Task<List<Book>> GetAll(CancellationToken cancellationToken);
    Task<Book?> GetById(Guid id, CancellationToken cancellationToken);
    Task Update(Book book, CancellationToken cancellationToken);
}
