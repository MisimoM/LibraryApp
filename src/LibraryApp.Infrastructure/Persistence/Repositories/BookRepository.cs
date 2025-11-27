using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Domain.Books;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Persistence.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BookRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Book book, CancellationToken cancellationToken)
    {
        await _dbContext.Books.AddAsync(book, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> Exists(string title, CancellationToken cancellationToken)
    {
        return await _dbContext.Books.AnyAsync(b  => b.Title == title, cancellationToken);
    }

    public async Task<Book?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Books
            .Include(b => b.Copies)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task Update(Book book, CancellationToken cancellationToken)
    {
        _dbContext.Update(book);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
