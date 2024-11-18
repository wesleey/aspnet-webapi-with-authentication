using Entities = Backend.Core.Domain.Entities;
using Backend.Core.Domain.Interfaces;
using Backend.Core.Domain.Enums;

namespace Backend.Core.Application.UseCases.User.CreateUser;

public class CreateUserUseCase(IUserRepository repository, IUnitOfWork unitOfWork)
{
    private readonly IUserRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(Entities.User user, CancellationToken cancellationToken)
    {
        if ((await _repository.GetByEmailAsync(user.Email, cancellationToken)) is not null)
            throw new ArgumentException("Email address is already in use");

        _repository.Create(user);
        await _unitOfWork.Commit(cancellationToken);
    }
}
