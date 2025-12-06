namespace LibraryApp.Application.Features.Loans.ReturnLoan;

public record ReturnLoanResponse(Guid LoanId,string BookTitle,DateTime ReturnedAt);
