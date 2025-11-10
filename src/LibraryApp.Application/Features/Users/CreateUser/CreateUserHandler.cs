using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.ResultPattern;
using LibraryApp.Domain.Users;

namespace LibraryApp.Application.Features.Users.CreateUser;

public class CreateUserHandler
{
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<CreateUserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var validationResult = CreateUserValidator.Validate(request);
        if (validationResult.IsFailure)
            return validationResult.Error;

        if (await _userRepository.Exists(request.Email, cancellationToken))
            return new ConflictError("UserConflict", $"A user with email '{request.Email}' already exists");

        var user = User.Create(
            request.Name,
            request.Email,
            request.Password
        );

        await _userRepository.Add(user, cancellationToken);

        return new CreateUserResponse(user.Id);
    }
}
