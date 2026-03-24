using System.Net.Mime;
using PitLaneShop.Services.Features.VisualizarVeiculos.Dtos;
using PitLaneShop.Services.Features.VisualizarVeiculos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PitLaneShop.Controllers;

[ApiController]
[Route("api/veiculos")]
[Produces(MediaTypeNames.Application.Json)]
public class VisualizarVeiculosController : ControllerBase
{
    private readonly IVisualizarVeiculosService _visualizarVeiculosService;

    public VisualizarVeiculosController(IVisualizarVeiculosService visualizarVeiculosService)
    {
        _visualizarVeiculosService = visualizarVeiculosService;
    }

    [HttpGet(Name = nameof(ListarVeiculosAsync))]
    [ProducesResponseType(typeof(IEnumerable<VisualizarVeiculoResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VisualizarVeiculoResponseDto>>> ListarVeiculosAsync(
        [FromQuery] string? filtro,
        CancellationToken cancellationToken)
    {
        var itens = await _visualizarVeiculosService.GetAllAsync(filtro, cancellationToken);
        return Ok(itens);
    }

    [HttpGet("{id:guid}", Name = nameof(BuscarVeiculoPorIdAsync))]
    [ProducesResponseType(typeof(VisualizarVeiculoDetalheResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VisualizarVeiculoDetalheResponseDto>> BuscarVeiculoPorIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var veiculo = await _visualizarVeiculosService.GetByIdAsync(id, cancellationToken);
        return veiculo is null ? NotFound() : Ok(veiculo);
    }
}
