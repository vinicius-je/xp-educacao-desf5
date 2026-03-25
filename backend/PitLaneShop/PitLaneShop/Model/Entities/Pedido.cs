using PitLaneShop.Model.Enums;

namespace PitLaneShop.Model.Entities;

public class Pedido : EntidadeBase
{
    public DateOnly DataPedido { get; set; }

    public decimal ValorTotal { get; set; }

    public decimal ValorDesconto { get; set; }

    public StatusPedido Status { get; set; }

    public Guid ClienteId { get; set; }

    public Cliente? Cliente { get; set; }

    public Guid? CodigoPromocionalId { get; set; }

    public CodigoPromocional? CodigoPromocional { get; set; }

    public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();

    public Pedido()
    {
    }

    public Pedido(DateOnly dataPedido, Guid clienteId, Guid? codigoPromocionalId = null)
    {
        DataPedido = dataPedido;
        ClienteId = clienteId;
        CodigoPromocionalId = codigoPromocionalId;
        Status = StatusPedido.EM_ANDAMENTO;
    }

    public void AdicionarItem(ItemPedido item)
    {
        Itens.Add(item);
    }

    public void CalcularTotal(decimal percentualDesconto = 0)
    {
        var subtotal = Itens.Sum(i => i.ValorTotal);
        ValorDesconto = Math.Round(subtotal * percentualDesconto / 100m, 2);
        ValorTotal = subtotal - ValorDesconto;
    }
}
