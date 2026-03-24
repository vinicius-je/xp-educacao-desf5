using System.Net.Mime;
using PitLaneShop.Model.Repositories;
using PitLaneShop.Services.Features.RealizarAluguel.Dtos;
using PitLaneShop.Services.Features.RealizarAluguel.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PitLaneShop.Controllers;

[ApiController]
[Route("api/alugueis")]
[Produces(MediaTypeNames.Application.Json)]
public class AlugueisController : ControllerBase
{
    private readonly IRealizarAluguelService _realizarAluguelService;
    private readonly IAluguelRepository _aluguelRepository;

    public AlugueisController(
        IRealizarAluguelService realizarAluguelService,
        IAluguelRepository aluguelRepository)
    {
        _realizarAluguelService = realizarAluguelService;
        _aluguelRepository = aluguelRepository;
    }

    [HttpGet("{id:guid}", Name = "BuscarAluguelPorId")]
    [ProducesResponseType(typeof(RealizarAluguelResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RealizarAluguelResponseDto>> BuscarAluguelPorIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var aluguel = await _aluguelRepository.GetByIdAsync(id, cancellationToken);
        if (aluguel is null) return NotFound();

        return Ok(new RealizarAluguelResponseDto
        {
            Id = aluguel.Id,
            DataRetirada = aluguel.DataRetirada,
            DataDevolucaoPrevista = aluguel.DataDevolucaoPrevista,
            Diarias = aluguel.Diarias,
            ValorTotal = aluguel.ValorTotal,
            CarroId = aluguel.CarroId,
            ClienteId = aluguel.ClienteId,
        });
    }

    [HttpPost(Name = nameof(RealizarAluguelAsync))]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(RealizarAluguelResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RealizarAluguelResponseDto>> RealizarAluguelAsync(
        [FromBody] RealizarAluguelRequestDto corpo,
        CancellationToken cancellationToken)
    {
        try
        {
            var resultado = await _realizarAluguelService.ExecutarAsync(corpo, cancellationToken);
            return CreatedAtRoute(
                "BuscarAluguelPorId",
                new { id = resultado.Id },
                resultado);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
