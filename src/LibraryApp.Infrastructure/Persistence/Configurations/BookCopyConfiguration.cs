using LibraryApp.Domain.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryApp.Infrastructure.Persistence.Configurations;

public class BookCopyConfiguration : IEntityTypeConfiguration<BookCopy>
{
    public void Configure(EntityTypeBuilder<BookCopy> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.BookId)
               .IsRequired();

        builder.Property(c => c.BookCopyId)
               .IsRequired();

        builder.Property(c => c.Status)
               .HasConversion<string>()
               .IsRequired();

        builder.Property(c => c.LastBorrowedAt);

        builder.HasIndex(c => c.BookId);
    }
}
