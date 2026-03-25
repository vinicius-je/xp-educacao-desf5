using AutoMapper;
using PitLaneShop.Services.Features.Produto.Dtos;
using ProdutoEntity = PitLaneShop.Model.Entities.Produto;

namespace PitLaneShop.Services.Features.Produto;

public class ProdutoMappingProfile : Profile
{
    public ProdutoMappingProfile()
    {
        CreateMap<ProdutoEntity, ProdutoResponseDto>();
        CreateMap<CreateProdutoDto, ProdutoEntity>();
        CreateMap<UpdateProdutoDto, ProdutoEntity>();
    }
}
