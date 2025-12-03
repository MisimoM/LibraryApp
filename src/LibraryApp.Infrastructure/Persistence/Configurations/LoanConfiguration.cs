using LibraryApp.Domain.Loans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryApp.Infrastructure.Persistence.Configurations;

public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.UserId)
               .IsRequired();

        builder.Property(l => l.BookId)
               .IsRequired();

        builder.Property(l => l.BookCopyId)
               .IsRequired();

        builder.Property(l => l.LoanDate)
               .IsRequired();

        builder.Property(l => l.DueDate)
               .IsRequired();

        builder.Property(l => l.ReturnedDate);

        builder.Property(l => l.IsActive)
               .IsRequired();

        builder.HasIndex(l => new { l.UserId, l.BookId, l.BookCopyId });
    }
}
