namespace PitLaneShop.Services.Features.RealizarAluguel.Dtos;

public class RealizarAluguelResponseDto
{
    public Guid Id { get; set; }

    public DateOnly DataRetirada { get; set; }

    public DateOnly DataDevolucaoPrevista { get; set; }

    public int Diarias { get; set; }

    public decimal ValorTotal { get; set; }

    public Guid CarroId { get; set; }

    public string CarroPlaca { get; set; } = string.Empty;

    public Guid ClienteId { get; set; }
}
