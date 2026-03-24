using PitLaneShop.Services.Features.RealizarAluguel.Dtos;

namespace PitLaneShop.Services.Features.RealizarAluguel.Interfaces;

public interface IRealizarAluguelService
{
    Task<RealizarAluguelResponseDto> ExecutarAsync(
        RealizarAluguelRequestDto request,
        CancellationToken cancellationToken = default);
}
