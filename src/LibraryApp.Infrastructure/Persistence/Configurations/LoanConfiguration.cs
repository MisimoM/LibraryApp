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
               .IsRequired()
               .HasConversion(
                   d => d.ToDateTime(TimeOnly.MinValue),
                   d => DateOnly.FromDateTime(d));

        builder.Property(l => l.DueDate)
               .IsRequired()
               .HasConversion(
                   d => d.ToDateTime(TimeOnly.MinValue),
                   d => DateOnly.FromDateTime(d));

        builder.Property(l => l.ReturnedDate)
               .HasConversion(
                   d => d.HasValue ? d.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null,
                   d => d.HasValue ? DateOnly.FromDateTime(d.Value) : (DateOnly?)null
               );

        builder.Property(l => l.IsActive)
               .IsRequired();

        builder.HasIndex(l => new { l.UserId, l.BookId, l.BookCopyId });
    }
}
