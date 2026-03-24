namespace PitLaneShop.Services.Abstractions;

public interface IBaseCrudService<TResponseDto, TCreateDto, TUpdateDto>
    where TResponseDto : class
{
    Task<TResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<TResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TResponseDto> CreateAsync(TCreateDto dto, CancellationToken cancellationToken = default);

    Task<TResponseDto?> UpdateAsync(Guid id, TUpdateDto dto, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<int> CountAsync(CancellationToken cancellationToken = default);
}
