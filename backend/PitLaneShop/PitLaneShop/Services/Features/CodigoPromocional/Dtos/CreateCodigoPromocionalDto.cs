namespace PitLaneShop.Services.Features.CodigoPromocional.Dtos;

public class CreateCodigoPromocionalDto
{
    public string Codigo { get; set; } = string.Empty;

    public decimal Desconto { get; set; }

    public bool EhValido { get; set; }
}
