using PitLaneShop.Services.Features.Pedido.Dtos;
using PedidoEntity = PitLaneShop.Model.Entities.Pedido;

namespace PitLaneShop.Services.Features.Pedido.Interfaces
{
    public interface IPedidoItemBuilder
    {
        Task AdicionarItensNoPedidoAsync(CreatePedidoDto dto, PedidoEntity pedido, CancellationToken cancellationToken);
    }
}