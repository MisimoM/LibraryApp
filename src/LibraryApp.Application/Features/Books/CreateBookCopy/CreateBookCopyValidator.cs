using LibraryApp.Application.Common.ResultPattern;

namespace LibraryApp.Application.Features.Books.CreateBookCopy;

public static class CreateBookCopyValidator
{
    private const int MaxCopiesPerRequest = 20;

    public static Result Validate(CreateBookCopyRequest request)
    {
        if (request.Count <= 0)
            return new ValidationError("InvalidCount", "Number of copies must be greater than zero");

        if (request.Count > MaxCopiesPerRequest)
            return new ValidationError("TooManyCopies", $"Cannot create more than {MaxCopiesPerRequest} copies at once");

        return Result.Success();
    }
}
