namespace LibraryApp.Application.Features.Loans.CreateLoan;

public record CreateLoanResponse(Guid LoanId, Guid UserId, Guid BookId, Guid BookCopyId, string BookTitle, DateTime DueDate);
