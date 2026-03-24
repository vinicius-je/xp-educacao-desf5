namespace PitLaneShop.Services.Features.Pedido.Dtos;

public class ItemPedidoResponseDto
{
    public Guid Id { get; set; }

    public Guid ProdutoId { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public decimal ValorUnitario { get; set; }

    public int Quantidade { get; set; }

    public decimal ValorTotal { get; set; }
}
