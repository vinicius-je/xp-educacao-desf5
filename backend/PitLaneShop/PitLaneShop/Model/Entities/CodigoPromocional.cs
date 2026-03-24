namespace PitLaneShop.Model.Entities;

public class CodigoPromocional : EntidadeBase
{
    public string Codigo { get; set; } = string.Empty;

    public decimal Desconto { get; set; }

    public bool EhValido { get; set; }

    public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public CodigoPromocional()
    {
    }

    public CodigoPromocional(string codigo, decimal desconto, bool ehValido)
    {
        Codigo = codigo;
        Desconto = desconto;
        EhValido = ehValido;
    }
}
