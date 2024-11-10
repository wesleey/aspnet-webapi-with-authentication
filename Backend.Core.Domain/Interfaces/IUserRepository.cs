using Backend.Core.Domain.Entities;

namespace Backend.Core.Domain.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}
