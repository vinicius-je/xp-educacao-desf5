using AutoMapper;
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
    protected readonly IMapper Mapper;

    protected BaseCrudService(IBaseRepository<TEntity> repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        Repository = repository;
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }

    public virtual async Task<TResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await Repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : Mapper.Map<TResponseDto>(entity);
    }

    public virtual async Task<List<TResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await Repository.GetAllAsync(cancellationToken);
        return list.Select(Mapper.Map<TResponseDto>).ToList();
    }

    public virtual async Task<TResponseDto> CreateAsync(TCreateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = Mapper.Map<TEntity>(dto);
        var created = await Repository.AddAsync(entity, cancellationToken);
        await UnitOfWork.SaveAsync(cancellationToken);
        return Mapper.Map<TResponseDto>(created);
    }

    public virtual async Task<TResponseDto?> UpdateAsync(Guid id, TUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await Repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return null;

        Mapper.Map(dto, entity);
        await Repository.UpdateAsync(entity, cancellationToken);
        await UnitOfWork.SaveAsync(cancellationToken);
        return Mapper.Map<TResponseDto>(entity);
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
}
