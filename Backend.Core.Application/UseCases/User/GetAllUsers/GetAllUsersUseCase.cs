using Entities = Backend.Core.Domain.Entities;
using Backend.Core.Domain.Interfaces;

namespace Backend.Core.Application.UseCases.User.GetAllUsers;

public class GetAllUsersUseCase(IUserRepository repository)
{
    private readonly IUserRepository _repository = repository;

    public async Task<List<Entities.User>> Execute(CancellationToken cancellationToken)
        => await _repository.GetAllAsync(cancellationToken);
}
