using System.Net.Mime;
using PitLaneShop.Services.Features.Carro.Dtos;
using PitLaneShop.Services.Features.Carro.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PitLaneShop.Controllers;

[ApiController]
[Route("api/carros")]
[Produces(MediaTypeNames.Application.Json)]
public class CarrosController : ControllerBase
{
    private readonly ICarroService _carroService;

    public CarrosController(ICarroService carroService)
    {
        _carroService = carroService;
    }

    [HttpGet(Name = nameof(ListarCarrosAsync))]
    [ProducesResponseType(typeof(IEnumerable<CarroResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CarroResponseDto>>> ListarCarrosAsync(
        CancellationToken cancellationToken)
    {
        var itens = await _carroService.GetAllAsync(cancellationToken);
        return Ok(itens);
    }

    [HttpGet("{id:guid}", Name = nameof(BuscarCarroPorIdAsync))]
    [ProducesResponseType(typeof(CarroResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CarroResponseDto>> BuscarCarroPorIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var carro = await _carroService.GetByIdAsync(id, cancellationToken);
        return carro is null ? NotFound() : Ok(carro);
    }

    [HttpPost(Name = nameof(CriarCarroAsync))]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(CarroResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CarroResponseDto>> CriarCarroAsync(
        [FromBody] CreateCarroDto corpo,
        CancellationToken cancellationToken)
    {
        var criado = await _carroService.CreateAsync(corpo, cancellationToken);
        return CreatedAtRoute(nameof(BuscarCarroPorIdAsync), new { id = criado.Id }, criado);
    }

    [HttpPut("{id:guid}", Name = nameof(AtualizarCarroAsync))]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(CarroResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CarroResponseDto>> AtualizarCarroAsync(
        Guid id,
        [FromBody] UpdateCarroDto corpo,
        CancellationToken cancellationToken)
    {
        var atualizado = await _carroService.UpdateAsync(id, corpo, cancellationToken);
        return atualizado is null ? NotFound() : Ok(atualizado);
    }

    [HttpGet("count", Name = nameof(ContarCarrosAsync))]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> ContarCarrosAsync(CancellationToken cancellationToken)
    {
        var total = await _carroService.CountAsync(cancellationToken);
        return Ok(total);
    }

    [HttpDelete("{id:guid}", Name = nameof(RemoverCarroAsync))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoverCarroAsync(Guid id, CancellationToken cancellationToken)
    {
        var removido = await _carroService.DeleteAsync(id, cancellationToken);
        return removido ? NoContent() : NotFound();
    }
}
