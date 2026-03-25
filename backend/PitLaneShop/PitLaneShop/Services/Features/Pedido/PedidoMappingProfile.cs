using AutoMapper;
using PitLaneShop.Model.Entities;
using PitLaneShop.Services.Features.Pedido.Dtos;
using PedidoEntity = PitLaneShop.Model.Entities.Pedido;

namespace PitLaneShop.Services.Features.Pedido;

public class PedidoMappingProfile : Profile
{
    public PedidoMappingProfile()
    {
        CreateMap<PedidoEntity, PedidoResponseDto>();

        CreateMap<ItemPedido, ItemPedidoResponseDto>()
            .ForMember(dest => dest.Imagem, opt => opt.MapFrom(src => src.Produto != null ? src.Produto.Imagem : string.Empty));
    }
}
