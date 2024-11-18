using Entities = Backend.Core.Domain.Entities;

namespace Backend.Core.Application.UseCases.User.CreateUser;

public class CreateUserMapper
{
    public static Entities.User MapTo(CreateUserRequest request)
        => new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password
        };

    public static CreateUserResponse MapFrom(Entities.User user)
        => new()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role.ToString()
        };
}
