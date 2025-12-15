using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Domain.Books;
using LibraryApp.Domain.Loans;
using LibraryApp.Domain.Users;
using LibraryApp.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<BookCopy> BookCopies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Loan> Loans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new BookCopyConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new LoanConfiguration());
    }
}
