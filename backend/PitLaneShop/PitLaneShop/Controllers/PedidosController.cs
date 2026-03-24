using System.Net.Mime;
using PitLaneShop.Services.Features.Pedido.Dtos;
using PitLaneShop.Services.Features.Pedido.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PitLaneShop.Controllers;

[ApiController]
[Route("api/pedidos")]
[Produces(MediaTypeNames.Application.Json)]
public class PedidosController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidosController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpGet(Name = nameof(ListarPedidosAsync))]
    [ProducesResponseType(typeof(IEnumerable<PedidoResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PedidoResponseDto>>> ListarPedidosAsync(
        CancellationToken cancellationToken)
    {
        var itens = await _pedidoService.GetAllAsync(cancellationToken);
        return Ok(itens);
    }

    [HttpGet("{id:guid}", Name = nameof(BuscarPedidoPorIdAsync))]
    [ProducesResponseType(typeof(PedidoResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PedidoResponseDto>> BuscarPedidoPorIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var pedido = await _pedidoService.GetByIdAsync(id, cancellationToken);
        return pedido is null ? NotFound() : Ok(pedido);
    }

    [HttpPost(Name = nameof(CriarPedidoAsync))]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PedidoResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PedidoResponseDto>> CriarPedidoAsync(
        [FromBody] CreatePedidoDto corpo,
        CancellationToken cancellationToken)
    {
        var criado = await _pedidoService.CreateAsync(corpo, cancellationToken);
        return CreatedAtRoute(nameof(BuscarPedidoPorIdAsync), new { id = criado.Id }, criado);
    }

    [HttpPut("{id:guid}", Name = nameof(AtualizarPedidoAsync))]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PedidoResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PedidoResponseDto>> AtualizarPedidoAsync(
        Guid id,
        [FromBody] UpdatePedidoDto corpo,
        CancellationToken cancellationToken)
    {
        var atualizado = await _pedidoService.UpdateAsync(id, corpo, cancellationToken);
        return atualizado is null ? NotFound() : Ok(atualizado);
    }

    [HttpGet("count", Name = nameof(ContarPedidosAsync))]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> ContarPedidosAsync(CancellationToken cancellationToken)
    {
        var total = await _pedidoService.CountAsync(cancellationToken);
        return Ok(total);
    }

    [HttpDelete("{id:guid}", Name = nameof(RemoverPedidoAsync))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoverPedidoAsync(Guid id, CancellationToken cancellationToken)
    {
        var removido = await _pedidoService.DeleteAsync(id, cancellationToken);
        return removido ? NoContent() : NotFound();
    }
}
