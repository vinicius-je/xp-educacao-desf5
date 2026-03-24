using PitLaneShop.Services.Abstractions;
using PitLaneShop.Services.Features.Cliente.Dtos;

namespace PitLaneShop.Services.Features.Cliente.Interfaces;

public interface IClienteService : IBaseCrudService<ClienteResponseDto, CreateClienteDto, UpdateClienteDto>
{
    Task<ClienteResponseDto?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
