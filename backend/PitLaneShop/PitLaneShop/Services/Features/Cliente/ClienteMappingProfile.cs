using AutoMapper;
using PitLaneShop.Services.Features.Cliente.Dtos;
using ClienteEntity = PitLaneShop.Model.Entities.Cliente;

namespace PitLaneShop.Services.Features.Cliente;

public class ClienteMappingProfile : Profile
{
    public ClienteMappingProfile()
    {
        CreateMap<ClienteEntity, ClienteResponseDto>();
        CreateMap<CreateClienteDto, ClienteEntity>();
        CreateMap<UpdateClienteDto, ClienteEntity>();
    }
}
