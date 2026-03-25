using AutoMapper;
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

    public ClienteService(IClienteRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        : base(repository, unitOfWork, mapper)
    {
        _clienteRepository = repository;
    }

    public async Task<ClienteResponseDto?> GetByEmailAsync(
        string email, CancellationToken cancellationToken = default)
    {
        var entity = await _clienteRepository.GetByEmailAsync(email, cancellationToken);
        return entity is null ? null : Mapper.Map<ClienteResponseDto>(entity);
    }
}
