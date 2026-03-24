namespace PitLaneShop.Services.Features.CodigoPromocional.Dtos;

public class CodigoPromocionalResponseDto
{
    public Guid Id { get; set; }

    public string Codigo { get; set; } = string.Empty;

    public decimal Desconto { get; set; }

    public bool EhValido { get; set; }
}
