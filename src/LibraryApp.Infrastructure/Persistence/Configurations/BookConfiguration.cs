using LibraryApp.Domain.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryApp.Infrastructure.Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(b => b.Author)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(b => b.Isbn)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(b => b.Publisher)
               .HasMaxLength(100);

        builder.Property(b => b.Description)
               .HasMaxLength(1000);

        builder.Property(b => b.CoverImageUrl)
               .HasMaxLength(500);

        builder.Property(b => b.Category)
               .HasConversion<string>()
               .IsRequired();

        builder.Property(b => b.Language)
               .HasConversion<string>()
               .IsRequired();

        builder.Property(b => b.BorrowCount)
               .IsRequired();

        builder.HasMany(b => b.Copies)
               .WithOne()
               .HasForeignKey(c => c.BookId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
