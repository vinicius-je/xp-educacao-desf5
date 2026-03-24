using PitLaneShop.Model.Repositories;
using PitLaneShop.Services.Abstractions;
using PitLaneShop.Services.Features.Cliente.Dtos;
using PitLaneShop.Services.Features.Cliente.Interfaces;
using ClienteEntity = PitLaneShop.Model.Entities.Cliente;

namespace PitLaneShop.Services.Features.Cliente.Implementation;

public class ClienteService
    : BaseCrudService<ClienteEntity, ClienteResponseDto, CreateClienteDto, UpdateClienteDto>, IClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository repository, IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
        _clienteRepository = repository;
    }

    public async Task<ClienteResponseDto?> GetByEmailAsync(
        string email, CancellationToken cancellationToken = default)
    {
        var entity = await _clienteRepository.GetByEmailAsync(email, cancellationToken);
        return entity is null ? null : MapToResponse(entity);
    }

    protected override ClienteResponseDto MapToResponse(ClienteEntity entity) => new()
    {
        Id = entity.Id,
        Nome = entity.Nome,
        Email = entity.Email,
        Telefone = entity.Telefone
    };

    protected override ClienteEntity MapFromCreate(CreateClienteDto dto) =>
        new(dto.Nome, dto.Email, dto.Telefone);

    protected override void ApplyUpdate(ClienteEntity entity, UpdateClienteDto dto)
    {
        entity.Nome = dto.Nome;
        entity.Email = dto.Email;
        entity.Telefone = dto.Telefone;
    }
}
