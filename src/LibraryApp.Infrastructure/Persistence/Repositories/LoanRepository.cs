using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Domain.Loans;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Persistence.Repositories;

public class LoanRepository : ILoanRepository
{

    private readonly ApplicationDbContext _dbContext;

    public LoanRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Loan loan, CancellationToken cancellationToken)
    {
        await _dbContext.Loans.AddAsync(loan, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Loan?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Loans.FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task Update(Loan loan, CancellationToken cancellationToken)
    {
        _dbContext.Update(loan);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
