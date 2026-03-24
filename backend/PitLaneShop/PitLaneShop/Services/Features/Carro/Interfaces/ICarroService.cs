using PitLaneShop.Services.Abstractions;
using PitLaneShop.Services.Features.Carro.Dtos;

namespace PitLaneShop.Services.Features.Carro.Interfaces;

public interface ICarroService : IBaseCrudService<CarroResponseDto, CreateCarroDto, UpdateCarroDto>
{
}
