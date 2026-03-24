namespace PitLaneShop.Model.Entities;

public class TarifaDiaria : EntidadeBase
{
    public decimal ValorDiaria { get; set; }

    public decimal ValorMulta { get; set; }

    public bool EhValorDiariaVigente { get; set; }

    public DateOnly DataInicioVigencia { get; set; }

    public DateOnly? DataFimVigencia { get; set; }

    public Guid VeiculoModeloId { get; set; }

    public VeiculoModelo? VeiculoModelo { get; set; }
}
