using LibraryApp.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.Features.Loans.GetLoans;

public class GetLoansHandler
{
    private readonly IApplicationDbContext _dbContext;

    public GetLoansHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GetLoansResponse>> Handle(CancellationToken cancellationToken)
    {
        var loans = await _dbContext.Loans
            .Include(l => l.User)
            .Include(l => l.Book)
                .ThenInclude(b => b.Copies)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        foreach (var loan in loans)
        {
            var copy = loan.Book.Copies.FirstOrDefault(c => c.Id == loan.BookCopyId);
        }

        return loans.Select(l => new GetLoansResponse(
            l.Id,
            l.UserId,
            l.BookId,
            l.BookCopyId,
            l.Status.ToString(),
            l.DueDate
        ));
    }
}
