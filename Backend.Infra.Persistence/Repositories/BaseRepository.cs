using Backend.Core.Domain.Entities;
using Backend.Core.Domain.Interfaces;
using Backend.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infra.Persistence.Repositories;

public class BaseRepository<T>(AppDbContext context) : IBaseRepository<T> where T : BaseEntity
{
    protected AppDbContext _context = context;

    public void Create(T entity)
    {
        entity.Created = DateTimeOffset.UtcNow;
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        entity.Updated = DateTimeOffset.UtcNow;
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
        => _context.Set<T>().Remove(entity);

    public async Task<T?> GetAsync(int id, CancellationToken cancellationToken)
        => await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        => await _context.Set<T>().ToListAsync(cancellationToken);
}
