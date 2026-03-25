using AutoMapper;
using PitLaneShop.Model.Entities;
using PitLaneShop.Model.Repositories;
using PitLaneShop.Services.Abstractions;
using PitLaneShop.Services.Features.Pedido.Dtos;
using PitLaneShop.Services.Features.Pedido.Interfaces;
using PedidoEntity = PitLaneShop.Model.Entities.Pedido;

namespace PitLaneShop.Services.Features.Pedido.Implementation;

public class PedidoService
    : BaseCrudService<PedidoEntity, PedidoResponseDto, CreatePedidoDto, UpdatePedidoDto>, IPedidoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly ICodigoPromocionalRepository _codigoPromocionalRepository;

    public PedidoService(
        IPedidoRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IProdutoRepository produtoRepository,
        ICodigoPromocionalRepository codigoPromocionalRepository)
        : base(repository, unitOfWork, mapper)
    {
        _produtoRepository = produtoRepository;
        _codigoPromocionalRepository = codigoPromocionalRepository;
    }

    public override async Task<PedidoResponseDto> CreateAsync(
        CreatePedidoDto dto,
        CancellationToken cancellationToken = default)
    {
        var pedido = new PedidoEntity(DateOnly.FromDateTime(DateTime.UtcNow), dto.ClienteId, dto.CodigoPromocionalId);
        await AdicionarItensNoPedito(dto, pedido, cancellationToken);
        decimal percentualDesconto = await VerificarDesconto(dto, cancellationToken);

        pedido.CalcularTotal(percentualDesconto);

        await Repository.AddAsync(pedido, cancellationToken);
        await UnitOfWork.SaveAsync(cancellationToken);

        return Mapper.Map<PedidoResponseDto>(pedido);
    }

    public override async Task<PedidoResponseDto?> UpdateAsync(
        Guid id,
        UpdatePedidoDto dto,
        CancellationToken cancellationToken = default)
    {
        var entity = await Repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return null;

        entity.Status = dto.Status;
        entity.CodigoPromocionalId = dto.CodigoPromocionalId;

        await Repository.UpdateAsync(entity, cancellationToken);
        await UnitOfWork.SaveAsync(cancellationToken);
        return Mapper.Map<PedidoResponseDto>(entity);
    }

    private async Task<decimal> VerificarDesconto(CreatePedidoDto dto, CancellationToken cancellationToken)
    {
        decimal percentualDesconto = 0;
        if (dto.CodigoPromocionalId.HasValue)
        {
            var codigo = await _codigoPromocionalRepository.GetByIdAsync(
                dto.CodigoPromocionalId.Value, cancellationToken);

            if (codigo is not null && codigo.EhValido)
                percentualDesconto = codigo.Desconto;
        }

        return percentualDesconto;
    }

    private async Task AdicionarItensNoPedito(CreatePedidoDto dto, PedidoEntity pedido, CancellationToken cancellationToken)
    {
        foreach (var itemDto in dto.Itens)
        {
            var produto = await _produtoRepository.GetByIdAsync(itemDto.ProdutoId, cancellationToken)
                ?? throw new InvalidOperationException($"Produto '{itemDto.ProdutoId}' não encontrado.");

            if (produto.QuantidadeEstoque < itemDto.Quantidade)
                throw new InvalidOperationException(
                    $"Estoque insuficiente para '{produto.Nome}'. Disponível: {produto.QuantidadeEstoque}, solicitado: {itemDto.Quantidade}.");

            produto.DecrementarEstoque(itemDto.Quantidade);
            var item = new ItemPedido(produto.Preco, itemDto.Quantidade, produto.Nome, pedido.Id, produto.Id);
            pedido.AdicionarItem(item);
        }
    }
}
