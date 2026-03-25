using AutoMapper;
using PitLaneShop.Model.Repositories;
using PitLaneShop.Services.Abstractions;
using PitLaneShop.Services.Features.Produto.Dtos;
using PitLaneShop.Services.Features.Produto.Interfaces;
using ProdutoEntity = PitLaneShop.Model.Entities.Produto;

namespace PitLaneShop.Services.Features.Produto.Implementation;

public class ProdutoService
    : BaseCrudService<ProdutoEntity, ProdutoResponseDto, CreateProdutoDto, UpdateProdutoDto>, IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        : base(repository, unitOfWork, mapper)
    {
        _produtoRepository = repository;
    }

    public async Task<List<ProdutoResponseDto>> GetByNomeAsync(
        string nome, CancellationToken cancellationToken = default)
    {
        var list = await _produtoRepository.GetByNomeAsync(nome, cancellationToken);
        return list.Select(Mapper.Map<ProdutoResponseDto>).ToList();
    }
}
