using PitLaneShop.Model.Enums;

namespace PitLaneShop.Model.Entities;

public class Produto : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;

    public string Imagem { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public decimal Preco { get; set; }

    public int QuantidadeEstoque { get; set; }

    public CategoriaProduto Categoria { get; set; }

    public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();

    public Produto()
    {
    }

    public Produto(string nome, string imagem, string descricao, decimal preco, int quantidadeEstoque, CategoriaProduto categoria)
    {
        Nome = nome;
        Imagem = imagem;
        Descricao = descricao;
        Preco = preco;
        QuantidadeEstoque = quantidadeEstoque;
        Categoria = categoria;
    }

    public void DecrementarEstoque(int quantidade)
    {
        if (quantidade <= QuantidadeEstoque)
        {
            QuantidadeEstoque -= quantidade;
        }
    }
}
