namespace PitLaneShop.Model.Entities;

public class Aluguel : EntidadeBase
{
    public Aluguel()
    {
    }

    public DateOnly DataRetirada { get; set; }

    public DateOnly DataDevolucaoPrevista { get; set; }

    public DateOnly? DataDevolucao { get; set; }

    public int Diarias { get; set; }

    public decimal ValorTotal { get; set; }

    public decimal ValorMulta { get; set; }

    public Guid ClienteId { get; set; }

    public Cliente? Cliente { get; set; }

    public Guid CarroId { get; set; }

    public Carro? Carro { get; set; }

    public Aluguel(DateOnly dataRetirada, DateOnly dataDevolucaoPrevista, int diarias, Guid carroId, Guid clienteId, decimal valorDiaria)
    {
        DataRetirada = dataRetirada;
        DataDevolucaoPrevista = dataDevolucaoPrevista;
        Diarias = diarias;
        CarroId = carroId;
        ClienteId = clienteId;
        ValorTotal = diarias * valorDiaria;
    }
}
