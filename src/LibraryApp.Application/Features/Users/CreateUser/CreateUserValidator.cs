using LibraryApp.Application.Common.ResultPattern;

namespace LibraryApp.Application.Features.Users.CreateUser;

public class CreateUserValidator
{
    public static Result Validate(CreateUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return new ValidationError("EmptyName", "Name cannot be empty");
        if (string.IsNullOrWhiteSpace(request.Email))
            return new ValidationError("EmptyEmail", "Email cannot be empty");
        if (string.IsNullOrWhiteSpace(request.Password))
            return new ValidationError("EmptyPassword", "Password cannot be empty");

        return Result.Success();
    }
}
