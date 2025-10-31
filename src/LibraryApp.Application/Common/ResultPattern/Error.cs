namespace LibraryApp.Application.Common.ResultPattern;
public enum ErrorType { BadRequest, NotFound, Conflict, Unauthorized, Unknown }
public record Error(string Code, string? Description = null, ErrorType Type = ErrorType.BadRequest)
{
    public static readonly Error None = new(string.Empty, null, ErrorType.Unknown);
}
