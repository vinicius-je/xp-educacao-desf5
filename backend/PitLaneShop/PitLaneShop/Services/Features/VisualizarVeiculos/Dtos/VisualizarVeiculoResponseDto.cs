namespace PitLaneShop.Services.Features.VisualizarVeiculos.Dtos;

public class VisualizarVeiculoResponseDto
{
    public Guid Id { get; set; }
    public string Imagem { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;

    public string Modelo { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string Categoria { get; set; } = string.Empty;

    public decimal? ValorTarifaDiaria { get; set; }

    public int QuantidadeCarrosDisponiveis { get; set; }
}
