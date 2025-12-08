using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Domain.Loans;

namespace LibraryApp.Application.Features.Loans.GetLoans;

public class GetLoansHandler
{
    private readonly ILoanRepository _loanRepository;

    public GetLoansHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<IEnumerable<GetLoansResponse>> Handle(CancellationToken cancellationToken)
    {
        var loans = await _loanRepository.GetAll(cancellationToken);

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
