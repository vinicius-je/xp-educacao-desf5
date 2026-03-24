namespace PitLaneShop.Services.Features.RealizarAluguel.Dtos;

public class RealizarAluguelRequestDto
{
    public Guid VeiculoModeloId { get; set; }

    public Guid ClienteId { get; set; }

    public DateOnly DataRetirada { get; set; }

    public DateOnly DataDevolucao { get; set; }
}
