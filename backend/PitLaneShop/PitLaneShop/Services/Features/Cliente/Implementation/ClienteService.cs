using PitLaneShop.Model.Repositories;
using PitLaneShop.Services.Abstractions;
using PitLaneShop.Services.Features.Cliente.Dtos;
using PitLaneShop.Services.Features.Cliente.Interfaces;
using ClienteEntity = PitLaneShop.Model.Entities.Cliente;

namespace PitLaneShop.Services.Features.Cliente.Implementation;

public class ClienteService
    : BaseCrudService<ClienteEntity, ClienteResponseDto, CreateClienteDto, UpdateClienteDto>, IClienteService
{
    public ClienteService(IClienteRepository repository, IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
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
