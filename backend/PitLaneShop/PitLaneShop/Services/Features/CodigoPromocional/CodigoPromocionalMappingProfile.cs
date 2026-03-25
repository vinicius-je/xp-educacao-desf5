using AutoMapper;
using PitLaneShop.Services.Features.CodigoPromocional.Dtos;
using CodigoPromocionalEntity = PitLaneShop.Model.Entities.CodigoPromocional;

namespace PitLaneShop.Services.Features.CodigoPromocional;

public class CodigoPromocionalMappingProfile : Profile
{
    public CodigoPromocionalMappingProfile()
    {
        CreateMap<CodigoPromocionalEntity, CodigoPromocionalResponseDto>();
        CreateMap<CreateCodigoPromocionalDto, CodigoPromocionalEntity>();
        CreateMap<UpdateCodigoPromocionalDto, CodigoPromocionalEntity>();
    }
}
