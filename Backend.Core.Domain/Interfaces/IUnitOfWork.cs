namespace Backend.Core.Domain.Interfaces;

public interface IUnitOfWork
{
    Task Commit(CancellationToken cancellationToken);
}
