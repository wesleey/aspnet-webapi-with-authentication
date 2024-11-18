using Backend.Core.Domain.Enums;
using Backend.Core.Domain.ValueObjects;

namespace Backend.Core.Domain.Entities;

public class User : BaseEntity
{
    private string _firstName = string.Empty;
    public required string FirstName
    {
        get => _firstName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("First name cannot be empty");

            if (value.Length < 3)
                throw new ArgumentException("First name must be at least 3 characters");

            _firstName = value;
        }
    }

    private string _lastName = string.Empty;
    public required string LastName
    {
        get => _lastName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Last name cannot be empty");

            if (value.Length < 3)
                throw new ArgumentException("Last name must be at least 3 characters");

            _lastName = value;
        }
    }

    private string _email = string.Empty;
    public required string Email
    {
        get => _email;
        set
        {
            var email = new Email(value);
            _email = email.Address;
        }
    }

    private string _password = string.Empty;
    public required string Password
    {
        get => _password;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Password cannot be empty");

            if (value.Length < 8)
                throw new ArgumentException("Password must be at least 8 characters");

            _password = value;
        }
    }

    public Role Role { get; set; } = Role.User;
}
