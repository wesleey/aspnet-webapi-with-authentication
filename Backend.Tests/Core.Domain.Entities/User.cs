using Backend.Core.Domain.Entities;

namespace Backend.Tests.Core.Domain.Entities;

public class UserTests
{
    private const string FirstName = "John";
    private const string LastName = "Doe";
    private const string Email = "john.doe@email.com";
    private const string Password = "p@ssw0rd";

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Should_ThrowException_When_FirstNameIsNullOrEmpty(string firstName)
        => Assert.Throws<ArgumentException>(() => new User
        {
            FirstName = firstName,
            LastName = LastName,
            Email = Email,
            Password = Password
        });

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Should_ThrowException_When_LastNameIsNullOrEmpty(string lastName)
        => Assert.Throws<ArgumentException>(() => new User
        {
            FirstName = FirstName,
            LastName = lastName,
            Email = Email,
            Password = Password
        });

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Should_ThrowException_When_EmailIsNullOrEmpty(string email)
        => Assert.Throws<ArgumentException>(() => new User
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = email,
            Password = Password
        });

    [Fact]
    public void Should_ThrowException_When_EmailFormatIsInvalid()
        => Assert.Throws<ArgumentException>(() => new User
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = "invalid_email",
            Password = Password
        });

    [Theory]
    [InlineData("")]
    [InlineData("p@ss")]
    public void Should_ThrowException_When_PasswordHasLessThan8Characters(string password)
        => Assert.Throws<ArgumentException>(() => new User
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Password = password
        });

    [Fact]
    public void Should_CreateUser_When_AllPropertiesAreValid()
    {
        var user = new User
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Password = Password
        };

        Assert.Equal(FirstName, user.FirstName);
        Assert.Equal(LastName, user.LastName);
        Assert.Equal(Email, user.Email);
        Assert.Equal(Password, user.Password);
    }
}
