using PitLaneShop.Model.Repositories;
using PitLaneShop.Services.Features.VisualizarVeiculos.Dtos;

namespace PitLaneShop.Services.Features.VisualizarVeiculos.Interfaces;

public interface IVisualizarVeiculosService
{
    Task<List<VisualizarVeiculoResponseDto>> GetAllAsync(string? filtro = null, CancellationToken cancellationToken = default);

    Task<VisualizarVeiculoDetalheResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
