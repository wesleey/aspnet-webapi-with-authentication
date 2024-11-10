using Backend.Core.Domain.Auth.Interfaces;
using Entities = Backend.Core.Domain.Entities;
using Backend.Core.Domain.Interfaces;

namespace Backend.Core.Application.UseCases.Auth.Login;

public class LoginUseCase(IUserRepository repository, IJwtTokenGenerator jwtTokenGenerator)
{
    private readonly IUserRepository _repository = repository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<(string, Entities.User)> Execute(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByEmailAsync(email, cancellationToken)
            ?? throw new ArgumentException("Invalid email");

        if (user.Password != password)
            throw new ArgumentException("Invalid password");

        var token = _jwtTokenGenerator.Generate(user.Id, user.Email);

        return (token, user);
    }
}
