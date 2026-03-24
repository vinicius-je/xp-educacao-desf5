namespace PitLaneShop.Services.Features.Pedido.Dtos;

public class CreatePedidoDto
{
    public Guid ClienteId { get; set; }

    public Guid? CodigoPromocionalId { get; set; }

    public List<CreateItemPedidoDto> Itens { get; set; } = new();
}
