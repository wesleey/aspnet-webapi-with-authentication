using Entities = Backend.Core.Domain.Entities;
using Backend.Core.Domain.Interfaces;

namespace Backend.Core.Application.UseCases.User.GetUser;

public class GetUserUseCase(IUserRepository repository)
{
    private readonly IUserRepository _repository = repository;

    public async Task<Entities.User> Execute(int id, CancellationToken cancellationToken)
        => await _repository.GetAsync(id, cancellationToken)
            ?? throw new ArgumentException("User not found");
}
