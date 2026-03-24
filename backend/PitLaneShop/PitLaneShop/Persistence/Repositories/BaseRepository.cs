using PitLaneShop.Model.Entities;
using PitLaneShop.Model.Repositories;
using PitLaneShop.Persistence;
using Microsoft.EntityFrameworkCore;

namespace PitLaneShop.Persistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : EntidadeBase
{
    protected readonly PitLaneShopDbContext Context;
    protected readonly DbSet<T> DbSet;

    public BaseRepository(PitLaneShopDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FindAsync([id], cancellationToken);
    }

    public virtual async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity.DataAtualizacao == default)
            entity.AtualizarDataAtualizacao();

        await DbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    public virtual Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.AtualizarDataAtualizacao();
        DbSet.Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return false;

        await DeleteAsync(entity, cancellationToken);
        return true;
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.CountAsync(cancellationToken);
    }
}
