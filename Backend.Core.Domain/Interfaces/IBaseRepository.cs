using Backend.Core.Domain.Entities;

namespace Backend.Core.Domain.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T?> GetAsync(int id, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
}
