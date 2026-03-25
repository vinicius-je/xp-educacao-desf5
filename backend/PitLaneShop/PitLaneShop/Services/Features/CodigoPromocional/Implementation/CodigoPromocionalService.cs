using AutoMapper;
using PitLaneShop.Model.Repositories;
using PitLaneShop.Services.Abstractions;
using PitLaneShop.Services.Features.CodigoPromocional.Dtos;
using PitLaneShop.Services.Features.CodigoPromocional.Interfaces;
using CodigoPromocionalEntity = PitLaneShop.Model.Entities.CodigoPromocional;

namespace PitLaneShop.Services.Features.CodigoPromocional.Implementation;

public class CodigoPromocionalService
    : BaseCrudService<CodigoPromocionalEntity, CodigoPromocionalResponseDto, CreateCodigoPromocionalDto, UpdateCodigoPromocionalDto>,
      ICodigoPromocionalService
{
    public CodigoPromocionalService(ICodigoPromocionalRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        : base(repository, unitOfWork, mapper)
    {
    }
}
