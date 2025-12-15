using LibraryApp.Domain.Books;
using LibraryApp.Domain.Loans;
using LibraryApp.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Book> Books { get; }
    DbSet<BookCopy> BookCopies { get; }
    DbSet<User> Users { get; }
    DbSet<Loan> Loans { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
