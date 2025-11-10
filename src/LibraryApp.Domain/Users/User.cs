namespace LibraryApp.Domain.Users;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public UserRole Role { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsActive { get; private set; }

    private User(string name, string email, string password, UserRole role)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
        Role = role;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public static User Create(string name, string email, string password, UserRole role = UserRole.Member)
    {
        return new User(name, email, password, role);
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new InvalidOperationException("User is already inactive.");

        IsActive = false;
    }

}
