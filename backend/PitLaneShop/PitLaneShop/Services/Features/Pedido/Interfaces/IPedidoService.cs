using PitLaneShop.Services.Abstractions;
using PitLaneShop.Services.Features.Pedido.Dtos;

namespace PitLaneShop.Services.Features.Pedido.Interfaces;

public interface IPedidoService : IBaseCrudService<PedidoResponseDto, CreatePedidoDto, UpdatePedidoDto>
{
    Task<IEnumerable<PedidoResponseDto>> GetPedidosPorClienteIdAsync(Guid clienteId, CancellationToken cancellationToken);
}
