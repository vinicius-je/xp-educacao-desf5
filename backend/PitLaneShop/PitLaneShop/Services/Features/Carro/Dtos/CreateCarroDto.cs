using PitLaneShop.Model.Enums;

namespace PitLaneShop.Services.Features.Carro.Dtos;

public class CreateCarroDto
{
    public string Placa { get; set; } = string.Empty;

    public string Quilometragem { get; set; } = string.Empty;

    public StatusCarro Status { get; set; }

    public string Imagem { get; set; } = string.Empty;

    public Guid VeiculoModeloId { get; set; }
}
