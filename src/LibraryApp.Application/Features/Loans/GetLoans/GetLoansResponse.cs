namespace LibraryApp.Application.Features.Loans.GetLoans;

public record GetLoansResponse(Guid Id, Guid UserId, Guid BookId, Guid BookCopyId, string Status, DateTime DueDate);