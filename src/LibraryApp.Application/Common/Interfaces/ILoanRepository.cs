using LibraryApp.Domain.Loans;

namespace LibraryApp.Application.Common.Interfaces;

public interface ILoanRepository
{
    Task Add(Loan loan, CancellationToken cancellationToken);
    Task<Loan?> GetById(Guid id, CancellationToken cancellationToken);
    Task Update(Loan loan, CancellationToken cancellationToken);
}
