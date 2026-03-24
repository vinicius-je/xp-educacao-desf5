using PitLaneShop.Model.Entities;
using PitLaneShop.Model.Repositories;

namespace PitLaneShop.Services.Abstractions;

public abstract class BaseCrudService<TEntity, TResponseDto, TCreateDto, TUpdateDto>
    : IBaseCrudService<TResponseDto, TCreateDto, TUpdateDto>
    where TEntity : EntidadeBase, new()
    where TResponseDto : class
{
    protected readonly IBaseRepository<TEntity> Repository;
    protected readonly IUnitOfWork UnitOfWork;

    protected BaseCrudService(IBaseRepository<TEntity> repository, IUnitOfWork unitOfWork)
    {
        Repository = repository;
        UnitOfWork = unitOfWork;
    }

    public virtual async Task<TResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await Repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : MapToResponse(entity);
    }

    public virtual async Task<List<TResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await Repository.GetAllAsync(cancellationToken);
        return list.Select(MapToResponse).ToList();
    }

    public virtual async Task<TResponseDto> CreateAsync(TCreateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = MapFromCreate(dto);
        var created = await Repository.AddAsync(entity, cancellationToken);
        await UnitOfWork.SaveAsync(cancellationToken);
        return MapToResponse(created);
    }

    public virtual async Task<TResponseDto?> UpdateAsync(Guid id, TUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await Repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return null;

        ApplyUpdate(entity, dto);
        await Repository.UpdateAsync(entity, cancellationToken);
        await UnitOfWork.SaveAsync(cancellationToken);
        return MapToResponse(entity);
    }

    public virtual async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await Repository.DeleteByIdAsync(id, cancellationToken);
        if (result)
            await UnitOfWork.SaveAsync(cancellationToken);
        return result;
    }

    public virtual Task<int> CountAsync(CancellationToken cancellationToken = default)
        => Repository.CountAsync(cancellationToken);

    protected abstract TResponseDto MapToResponse(TEntity entity);

    protected abstract TEntity MapFromCreate(TCreateDto dto);

    protected abstract void ApplyUpdate(TEntity entity, TUpdateDto dto);
}
