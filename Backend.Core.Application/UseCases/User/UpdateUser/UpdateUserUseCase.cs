using Entities = Backend.Core.Domain.Entities;
using Backend.Core.Domain.Interfaces;

namespace Backend.Core.Application.UseCases.User.UpdateUser;

public class UpdateUserUseCase(IUserRepository repository, IUnitOfWork unitOfWork)
{
    private readonly IUserRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(Entities.User user, CancellationToken cancellationToken)
    {
        if ((await _repository.GetByEmailAsync(user.Email, cancellationToken)) is not null)
            throw new ArgumentException("Email address is already in use");

        _repository.Update(user);
        await _unitOfWork.Commit(cancellationToken);
    }
}
