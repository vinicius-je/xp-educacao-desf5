using PitLaneShop.Model.Enums;

namespace PitLaneShop.Services.Features.Pedido.Dtos;

public class PedidoResponseDto
{
    public Guid Id { get; set; }

    public DateOnly DataPedido { get; set; }

    public decimal ValorTotal { get; set; }

    public decimal ValorDesconto { get; set; }

    public StatusPedido Status { get; set; }

    public Guid ClienteId { get; set; }

    public Guid? CodigoPromocionalId { get; set; }

    public List<ItemPedidoResponseDto> Itens { get; set; } = new();
}
