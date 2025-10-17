﻿using LibraryApp.Domain.Books;
using LibraryApp.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<BookCopy> BookCopies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new BookCopyConfiguration());
    }
}
