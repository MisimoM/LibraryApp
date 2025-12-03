namespace LibraryApp.Application.Features.Loans.CreateLoan;

public record CreateLoanRequest(Guid UserId, Guid BookId, int BookCopyId);
