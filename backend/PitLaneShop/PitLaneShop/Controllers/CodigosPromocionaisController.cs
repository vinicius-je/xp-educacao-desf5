using System.Net.Mime;
using PitLaneShop.Services.Features.CodigoPromocional.Dtos;
using PitLaneShop.Services.Features.CodigoPromocional.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PitLaneShop.Controllers;

[ApiController]
[Route("api/codigos-promocionais")]
[Produces(MediaTypeNames.Application.Json)]
public class CodigosPromocionaisController : ControllerBase
{
    private readonly ICodigoPromocionalService _codigoPromocionalService;

    public CodigosPromocionaisController(ICodigoPromocionalService codigoPromocionalService)
    {
        _codigoPromocionalService = codigoPromocionalService;
    }

    [HttpGet(Name = nameof(ListarCodigosPromocionaisAsync))]
    [ProducesResponseType(typeof(IEnumerable<CodigoPromocionalResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CodigoPromocionalResponseDto>>> ListarCodigosPromocionaisAsync(
        CancellationToken cancellationToken)
    {
        var itens = await _codigoPromocionalService.GetAllAsync(cancellationToken);
        return Ok(itens);
    }

    [HttpGet("{id:guid}", Name = nameof(BuscarCodigoPromocionalPorIdAsync))]
    [ProducesResponseType(typeof(CodigoPromocionalResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CodigoPromocionalResponseDto>> BuscarCodigoPromocionalPorIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var codigo = await _codigoPromocionalService.GetByIdAsync(id, cancellationToken);
        return codigo is null ? NotFound() : Ok(codigo);
    }

    [HttpPost(Name = nameof(CriarCodigoPromocionalAsync))]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(CodigoPromocionalResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CodigoPromocionalResponseDto>> CriarCodigoPromocionalAsync(
        [FromBody] CreateCodigoPromocionalDto corpo,
        CancellationToken cancellationToken)
    {
        var criado = await _codigoPromocionalService.CreateAsync(corpo, cancellationToken);
        return CreatedAtRoute(nameof(BuscarCodigoPromocionalPorIdAsync), new { id = criado.Id }, criado);
    }

    [HttpPut("{id:guid}", Name = nameof(AtualizarCodigoPromocionalAsync))]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(CodigoPromocionalResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CodigoPromocionalResponseDto>> AtualizarCodigoPromocionalAsync(
        Guid id,
        [FromBody] UpdateCodigoPromocionalDto corpo,
        CancellationToken cancellationToken)
    {
        var atualizado = await _codigoPromocionalService.UpdateAsync(id, corpo, cancellationToken);
        return atualizado is null ? NotFound() : Ok(atualizado);
    }

    [HttpGet("count", Name = nameof(ContarCodigosPromocionaisAsync))]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> ContarCodigosPromocionaisAsync(CancellationToken cancellationToken)
    {
        var total = await _codigoPromocionalService.CountAsync(cancellationToken);
        return Ok(total);
    }

    [HttpDelete("{id:guid}", Name = nameof(RemoverCodigoPromocionalAsync))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoverCodigoPromocionalAsync(Guid id, CancellationToken cancellationToken)
    {
        var removido = await _codigoPromocionalService.DeleteAsync(id, cancellationToken);
        return removido ? NoContent() : NotFound();
    }
}
