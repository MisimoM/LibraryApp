using LibraryApp.Domain.Users;

namespace LibraryApp.Application.Common.Interfaces;

public interface IUserRepository
{
    Task Add(User user, CancellationToken cancellationToken);
    Task<bool> Exists(string email, CancellationToken cancellationToken);
}
