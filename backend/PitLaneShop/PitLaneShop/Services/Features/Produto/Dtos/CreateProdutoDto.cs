using PitLaneShop.Model.Enums;

namespace PitLaneShop.Services.Features.Produto.Dtos;

public class CreateProdutoDto
{
    public string Nome { get; set; } = string.Empty;

    public string Imagem { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public decimal Preco { get; set; }

    public int QuantidadeEstoque { get; set; }

    public CategoriaProduto Categoria { get; set; }
}
