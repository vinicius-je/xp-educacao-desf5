using PitLaneShop.Model.Repositories;
using PitLaneShop.Services.Abstractions;
using PitLaneShop.Services.Features.Produto.Dtos;
using PitLaneShop.Services.Features.Produto.Interfaces;
using ProdutoEntity = PitLaneShop.Model.Entities.Produto;

namespace PitLaneShop.Services.Features.Produto.Implementation;

public class ProdutoService
    : BaseCrudService<ProdutoEntity, ProdutoResponseDto, CreateProdutoDto, UpdateProdutoDto>, IProdutoService
{
    public ProdutoService(IProdutoRepository repository, IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
    }

    protected override ProdutoResponseDto MapToResponse(ProdutoEntity entity) => new()
    {
        Id = entity.Id,
        Nome = entity.Nome,
        Imagem = entity.Imagem,
        Descricao = entity.Descricao,
        Preco = entity.Preco,
        QuantidadeEstoque = entity.QuantidadeEstoque,
        Categoria = entity.Categoria
    };

    protected override ProdutoEntity MapFromCreate(CreateProdutoDto dto) =>
        new(dto.Nome, dto.Imagem, dto.Descricao, dto.Preco, dto.QuantidadeEstoque, dto.Categoria);

    protected override void ApplyUpdate(ProdutoEntity entity, UpdateProdutoDto dto)
    {
        entity.Nome = dto.Nome;
        entity.Imagem = dto.Imagem;
        entity.Descricao = dto.Descricao;
        entity.Preco = dto.Preco;
        entity.QuantidadeEstoque = dto.QuantidadeEstoque;
        entity.Categoria = dto.Categoria;
    }
}
