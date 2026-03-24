using PitLaneShop.Services.Abstractions;
using PitLaneShop.Services.Features.CodigoPromocional.Dtos;

namespace PitLaneShop.Services.Features.CodigoPromocional.Interfaces;

public interface ICodigoPromocionalService
    : IBaseCrudService<CodigoPromocionalResponseDto, CreateCodigoPromocionalDto, UpdateCodigoPromocionalDto>
{
}
