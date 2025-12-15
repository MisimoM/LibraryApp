using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.ResultPattern;
using LibraryApp.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.Features.Users.CreateUser;

public class CreateUserHandler
{
    private readonly IApplicationDbContext _dbContext;

    public CreateUserHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<CreateUserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var validationResult = CreateUserValidator.Validate(request);
        if (validationResult.IsFailure)
            return validationResult.Error;

        if (await _dbContext.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
            return new ConflictError("UserConflict", $"A user with email '{request.Email}' already exists");

        var user = User.Create(
            request.Name,
            request.Email,
            request.Password
        );

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateUserResponse(user.Id);
    }
}
