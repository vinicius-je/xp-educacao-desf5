using PitLaneShop.Services.Abstractions;
using PitLaneShop.Services.Features.Produto.Dtos;

namespace PitLaneShop.Services.Features.Produto.Interfaces;

public interface IProdutoService : IBaseCrudService<ProdutoResponseDto, CreateProdutoDto, UpdateProdutoDto>
{
}
