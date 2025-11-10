namespace LibraryApp.Application.Features.Users.CreateUser;

public record CreateUserRequest(
    string Name,
    string Email,
    string Password
);
