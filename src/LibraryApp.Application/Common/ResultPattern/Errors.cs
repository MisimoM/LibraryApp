namespace LibraryApp.Application.Common.ResultPattern;

public sealed record ValidationError(string Code, string? Description = null)
    : Error(Code, Description, ErrorType.BadRequest);

public sealed record NotFoundError(string Code, string? Description = null)
    : Error(Code, Description, ErrorType.NotFound);

public sealed record ConflictError(string Code, string? Description = null)
    : Error(Code, Description, ErrorType.Conflict);


