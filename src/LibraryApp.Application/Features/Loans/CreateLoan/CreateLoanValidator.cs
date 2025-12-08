using LibraryApp.Application.Common.ResultPattern;

namespace LibraryApp.Application.Features.Loans.CreateLoan;

public class CreateLoanValidator
{
    public static Result Validate(CreateLoanRequest request)
    {
        if (request.UserId == Guid.Empty)
            return new ValidationError("InvalidUserId", "UserId cannot be empty");

        if (request.BookId == Guid.Empty)
            return new ValidationError("InvalidBookId", "BookId cannot be empty");

        if (request.BookCopyId == Guid.Empty)
            return new ValidationError("InvalidBookCopyId", "BookCopyId cannot be empty");

        return Result.Success();
    }
}
