namespace PitLaneShop.Model.Entities;

public class ItemPedido : EntidadeBase
{
    public decimal ValorUnitario { get; set; }

    public decimal ValorTotal { get; set; }

    public int Quantidade { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public Guid PedidoId { get; set; }

    public Pedido? Pedido { get; set; }

    public Guid ProdutoId { get; set; }

    public Produto? Produto { get; set; }

    public ItemPedido()
    {
    }

    public ItemPedido(decimal valorUnitario, int quantidade, string descricao, Guid pedidoId, Guid produtoId)
    {
        ValorUnitario = valorUnitario;
        Quantidade = quantidade;
        Descricao = descricao;
        PedidoId = pedidoId;
        ProdutoId = produtoId;
        ValorTotal = valorUnitario * quantidade;
    }
}
