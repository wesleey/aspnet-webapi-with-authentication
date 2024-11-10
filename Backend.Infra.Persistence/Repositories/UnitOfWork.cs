using Backend.Core.Domain.Interfaces;
using Backend.Infra.Persistence.Context;

namespace Backend.Infra.Persistence.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;

    public async Task Commit(CancellationToken cancellationToken)
        => await _context.SaveChangesAsync(cancellationToken);
}
