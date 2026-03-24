using PitLaneShop.Model.Enums;

namespace PitLaneShop.Services.Features.Pedido.Dtos;

public class UpdatePedidoDto
{
    public StatusPedido Status { get; set; }

    public Guid? CodigoPromocionalId { get; set; }
}
