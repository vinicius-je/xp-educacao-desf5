using PitLaneShop.Model.Enums;
using PitLaneShop.Model.Entities;
using PitLaneShop.Model.Repositories;
using PitLaneShop.Services.Features.VisualizarVeiculos.Dtos;
using PitLaneShop.Services.Features.VisualizarVeiculos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PitLaneShop.Services.Features.VisualizarVeiculos.Implementation;

public class VisualizarVeiculosService : IVisualizarVeiculosService
{
    private readonly IVeiculoModeloRepository _veiculoModeloRepository;

    public VisualizarVeiculosService(IVeiculoModeloRepository veiculoModeloRepository)
    {
        _veiculoModeloRepository = veiculoModeloRepository;
    }

    public async Task<List<VisualizarVeiculoResponseDto>> GetAllAsync(string? filtro = null, CancellationToken cancellationToken = default)
    {
        var query = _veiculoModeloRepository.GetAllWithCarrosAndTarifas();

        if (!string.IsNullOrWhiteSpace(filtro))
            query = query.Where(v => v.Marca.Contains(filtro) || v.Modelo.Contains(filtro));

        var veiculos = await query
            .OrderBy(v => v.Marca)
            .ThenBy(v => v.Modelo)
            .ToListAsync(cancellationToken);

        return veiculos.Select(MapToResponse).ToList();
    }

    public async Task<VisualizarVeiculoDetalheResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var veiculoModelo = await _veiculoModeloRepository.GetAllWithCarrosAndTarifas()
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);

        if (veiculoModelo is null)
            return null;

        return MapToDetalheResponse(veiculoModelo);
    }

    private static VisualizarVeiculoDetalheResponseDto MapToDetalheResponse(VeiculoModelo veiculoModelo)
    {
        var baseDto = MapToResponse(veiculoModelo);
        var tarifaVigente = veiculoModelo.TarifasDiarias.FirstOrDefault();

        return new VisualizarVeiculoDetalheResponseDto
        {
            Id = baseDto.Id,
            Imagem = baseDto.Imagem,
            Marca = baseDto.Marca,
            Modelo = baseDto.Modelo,
            Status = baseDto.Status,
            Categoria = baseDto.Categoria,
            ValorTarifaDiaria = baseDto.ValorTarifaDiaria,
            QuantidadeCarrosDisponiveis = baseDto.QuantidadeCarrosDisponiveis,
            ValorMulta = tarifaVigente?.ValorMulta
        };
    }

    private static VisualizarVeiculoResponseDto MapToResponse(VeiculoModelo veiculoModelo)
    {
        var quantidadeCarrosDisponiveis = veiculoModelo.Carros.Count(c => c.Status == StatusCarro.Disponivel);
        var tarifaVigente = veiculoModelo.TarifasDiarias.First();

        return new VisualizarVeiculoResponseDto
        {
            Id = veiculoModelo.Id,
            Imagem = veiculoModelo.Carros.First().Imagem,
            Marca = veiculoModelo.Marca,
            Modelo = veiculoModelo.Modelo,
            Status = quantidadeCarrosDisponiveis > 0 ? StatusCarro.Disponivel.ToString() : StatusCarro.Alugado.ToString(),
            Categoria = veiculoModelo.Categoria.ToString(),
            ValorTarifaDiaria = tarifaVigente.ValorDiaria,
            QuantidadeCarrosDisponiveis = quantidadeCarrosDisponiveis
        };
    }
}
