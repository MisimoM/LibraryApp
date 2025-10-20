using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Domain.Books;
using System;

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
}
