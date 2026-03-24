using System.Net.Mime;
using PitLaneShop.Services.Features.Produto.Dtos;
using PitLaneShop.Services.Features.Produto.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PitLaneShop.Controllers;

[ApiController]
[Route("api/produtos")]
[Produces(MediaTypeNames.Application.Json)]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet(Name = nameof(ListarProdutosAsync))]
    [ProducesResponseType(typeof(IEnumerable<ProdutoResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProdutoResponseDto>>> ListarProdutosAsync(
        CancellationToken cancellationToken)
    {
        var itens = await _produtoService.GetAllAsync(cancellationToken);
        return Ok(itens);
    }

    [HttpGet("{id:guid}", Name = nameof(BuscarProdutoPorIdAsync))]
    [ProducesResponseType(typeof(ProdutoResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProdutoResponseDto>> BuscarProdutoPorIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var produto = await _produtoService.GetByIdAsync(id, cancellationToken);
        return produto is null ? NotFound() : Ok(produto);
    }

    [HttpPost(Name = nameof(CriarProdutoAsync))]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ProdutoResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProdutoResponseDto>> CriarProdutoAsync(
        [FromBody] CreateProdutoDto corpo,
        CancellationToken cancellationToken)
    {
        var criado = await _produtoService.CreateAsync(corpo, cancellationToken);
        return CreatedAtRoute(nameof(BuscarProdutoPorIdAsync), new { id = criado.Id }, criado);
    }

    [HttpPut("{id:guid}", Name = nameof(AtualizarProdutoAsync))]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ProdutoResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProdutoResponseDto>> AtualizarProdutoAsync(
        Guid id,
        [FromBody] UpdateProdutoDto corpo,
        CancellationToken cancellationToken)
    {
        var atualizado = await _produtoService.UpdateAsync(id, corpo, cancellationToken);
        return atualizado is null ? NotFound() : Ok(atualizado);
    }

    [HttpGet("count", Name = nameof(ContarProdutosAsync))]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> ContarProdutosAsync(CancellationToken cancellationToken)
    {
        var total = await _produtoService.CountAsync(cancellationToken);
        return Ok(total);
    }

    [HttpDelete("{id:guid}", Name = nameof(RemoverProdutoAsync))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoverProdutoAsync(Guid id, CancellationToken cancellationToken)
    {
        var removido = await _produtoService.DeleteAsync(id, cancellationToken);
        return removido ? NoContent() : NotFound();
    }
}
