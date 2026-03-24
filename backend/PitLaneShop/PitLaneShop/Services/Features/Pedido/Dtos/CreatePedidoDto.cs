namespace PitLaneShop.Services.Features.Pedido.Dtos;

public class CreatePedidoDto
{
    public DateOnly DataPedido { get; set; }

    public Guid ClienteId { get; set; }

    public Guid? CodigoPromocionalId { get; set; }
}
