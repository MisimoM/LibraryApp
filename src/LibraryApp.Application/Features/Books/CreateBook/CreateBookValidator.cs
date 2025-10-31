using LibraryApp.Application.Common.ResultPattern;

namespace LibraryApp.Application.Features.Books.CreateBook;

public class CreateBookValidator
{
    public static Result Validate(CreateBookRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return new ValidationError("EmptyTitle", "Title cannot be empty");

        if (string.IsNullOrWhiteSpace(request.Author))
            return new ValidationError("EmptyAuthor", "Author cannot be empty");

        if (string.IsNullOrWhiteSpace(request.Isbn))
            return new ValidationError("EmptyIsbn", "ISBN cannot be empty");

        if (string.IsNullOrWhiteSpace(request.Publisher))
            return new ValidationError("EmptyPublisher", "Publisher cannot be empty");

        if (request.PublicationDate > DateOnly.FromDateTime(DateTime.UtcNow))
            return new ValidationError("InvalidPublicationDate", "Publication date cannot be in the future");

        if (!Enum.IsDefined(request.Category))
            return new ValidationError("InvalidCategory", "Invalid book category");

        if (!Enum.IsDefined(request.Language))
            return new ValidationError("InvalidLanguage", "Invalid book language");

        return Result.Success();
    }
}
