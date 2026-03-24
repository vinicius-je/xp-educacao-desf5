using PitLaneShop.Model.Enums;

namespace PitLaneShop.Model.Entities;

public class Carro : EntidadeBase
{
    public string Placa { get; set; } = string.Empty;

    public string Quilometragem { get; set; } = string.Empty;

    public StatusCarro Status { get; set; }

    public string Imagem { get; set; } = string.Empty;

    public Guid VeiculoModeloId { get; set; }

    public VeiculoModelo? VeiculoModelo { get; set; }

    public ICollection<Aluguel> Alugueis { get; set; } = new List<Aluguel>();

    public Carro()
    {
    }

    public Carro(string placa, string quilometragem, StatusCarro status, Guid veiculoModeloId, string imagem = "")
    {
        Placa = placa;
        Quilometragem = quilometragem;
        Status = status;
        VeiculoModeloId = veiculoModeloId;
        Imagem = imagem;
    }

    public void AlugarCarro()
    {
        Status = StatusCarro.Alugado;
    }

    public void DevolverCarro(string novaQuilometragem)
    {
        Status = StatusCarro.Disponivel;
        Quilometragem = novaQuilometragem;
    }
}
