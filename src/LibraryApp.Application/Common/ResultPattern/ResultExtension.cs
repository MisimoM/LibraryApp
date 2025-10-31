using Microsoft.AspNetCore.Http;

namespace LibraryApp.Application.Common.ResultPattern;

public static class ErrorExtensions
{
    public static IResult ToHttpResult(this Error error) => error.Type switch
    {
        ErrorType.BadRequest => Results.BadRequest(new { error = error.Type.ToString(), error.Code, error.Description }),
        ErrorType.NotFound => Results.NotFound(new { error = error.Type.ToString(), error.Code, error.Description }),
        ErrorType.Conflict => Results.Conflict(new { error = error.Type.ToString(), error.Code, error.Description }),
        _ => Results.Problem(error.Description)
    };
}
