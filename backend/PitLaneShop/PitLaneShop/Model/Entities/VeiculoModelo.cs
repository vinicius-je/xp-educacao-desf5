using PitLaneShop.Model.Enums;

namespace PitLaneShop.Model.Entities;

public class VeiculoModelo : EntidadeBase
{
    public string Marca { get; set; } = string.Empty;

    public string Modelo { get; set; } = string.Empty;

    public CategoriaVeiculo Categoria { get; set; }

    public ICollection<Carro> Carros { get; set; } = new List<Carro>();

    public ICollection<TarifaDiaria> TarifasDiarias { get; set; } = new List<TarifaDiaria>();
}
