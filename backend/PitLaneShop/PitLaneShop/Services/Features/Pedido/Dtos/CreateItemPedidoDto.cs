namespace PitLaneShop.Services.Features.Pedido.Dtos;

public class CreateItemPedidoDto
{
    public Guid ProdutoId { get; set; }

    public int Quantidade { get; set; }
}
