using System.Net.Mime;
using PitLaneShop.Services.Features.Cliente.Dtos;
using PitLaneShop.Services.Features.Cliente.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PitLaneShop.Controllers;

[ApiController]
[Route("api/clientes")]
[Produces(MediaTypeNames.Application.Json)]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet(Name = nameof(ListarClientesAsync))]
    [ProducesResponseType(typeof(IEnumerable<ClienteResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ClienteResponseDto>>> ListarClientesAsync(
        CancellationToken cancellationToken)
    {
        var itens = await _clienteService.GetAllAsync(cancellationToken);
        return Ok(itens);
    }

    [HttpGet("{id:guid}", Name = nameof(BuscarClientePorIdAsync))]
    [ProducesResponseType(typeof(ClienteResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteResponseDto>> BuscarClientePorIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var cliente = await _clienteService.GetByIdAsync(id, cancellationToken);
        return cliente is null ? NotFound() : Ok(cliente);
    }

    [HttpPost(Name = nameof(CriarClienteAsync))]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ClienteResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClienteResponseDto>> CriarClienteAsync(
        [FromBody] CreateClienteDto corpo,
        CancellationToken cancellationToken)
    {
        var criado = await _clienteService.CreateAsync(corpo, cancellationToken);
        return CreatedAtRoute(nameof(BuscarClientePorIdAsync), new { id = criado.Id }, criado);
    }

    [HttpPut("{id:guid}", Name = nameof(AtualizarClienteAsync))]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ClienteResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteResponseDto>> AtualizarClienteAsync(
        Guid id,
        [FromBody] UpdateClienteDto corpo,
        CancellationToken cancellationToken)
    {
        var atualizado = await _clienteService.UpdateAsync(id, corpo, cancellationToken);
        return atualizado is null ? NotFound() : Ok(atualizado);
    }

    [HttpGet("count", Name = nameof(ContarClientesAsync))]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> ContarClientesAsync(CancellationToken cancellationToken)
    {
        var total = await _clienteService.CountAsync(cancellationToken);
        return Ok(total);
    }

    [HttpDelete("{id:guid}", Name = nameof(RemoverClienteAsync))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoverClienteAsync(Guid id, CancellationToken cancellationToken)
    {
        var removido = await _clienteService.DeleteAsync(id, cancellationToken);
        return removido ? NoContent() : NotFound();
    }
}
