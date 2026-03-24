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
        IProdutoRepository produtoRepository,
        ICodigoPromocionalRepository codigoPromocionalRepository)
        : base(repository, unitOfWork)
    {
        _produtoRepository = produtoRepository;
        _codigoPromocionalRepository = codigoPromocionalRepository;
    }

    public override async Task<PedidoResponseDto> CreateAsync(
        CreatePedidoDto dto,
        CancellationToken cancellationToken = default)
    {
        var pedido = new PedidoEntity(DateOnly.FromDateTime(DateTime.UtcNow), dto.ClienteId, dto.CodigoPromocionalId);

        foreach (var itemDto in dto.Itens)
        {
            var produto = await _produtoRepository.GetByIdAsync(itemDto.ProdutoId, cancellationToken)
                ?? throw new InvalidOperationException($"Produto '{itemDto.ProdutoId}' não encontrado.");

            var item = new ItemPedido(
                valorUnitario: produto.Preco,
                quantidade: itemDto.Quantidade,
                descricao: produto.Nome,
                pedidoId: pedido.Id,
                produtoId: produto.Id);

            pedido.AdicionarItem(item);
        }

        decimal percentualDesconto = 0;
        if (dto.CodigoPromocionalId.HasValue)
        {
            var codigo = await _codigoPromocionalRepository.GetByIdAsync(
                dto.CodigoPromocionalId.Value, cancellationToken);

            if (codigo is not null && codigo.EhValido)
                percentualDesconto = codigo.Desconto;
        }

        pedido.CalcularTotal(percentualDesconto);

        await Repository.AddAsync(pedido, cancellationToken);
        await UnitOfWork.SaveAsync(cancellationToken);

        return MapToResponse(pedido);
    }

    protected override PedidoResponseDto MapToResponse(PedidoEntity entity) => new()
    {
        Id = entity.Id,
        DataPedido = entity.DataPedido,
        ValorTotal = entity.ValorTotal,
        ValorDesconto = entity.ValorDesconto,
        Status = entity.Status,
        ClienteId = entity.ClienteId,
        CodigoPromocionalId = entity.CodigoPromocionalId,
        Itens = entity.Itens.Select(i => new ItemPedidoResponseDto
        {
            Id = i.Id,
            ProdutoId = i.ProdutoId,
            Descricao = i.Descricao,
            ValorUnitario = i.ValorUnitario,
            Quantidade = i.Quantidade,
            ValorTotal = i.ValorTotal
        }).ToList()
    };

    protected override PedidoEntity MapFromCreate(CreatePedidoDto dto) =>
        new(DateOnly.FromDateTime(DateTime.UtcNow), dto.ClienteId, dto.CodigoPromocionalId);

    protected override void ApplyUpdate(PedidoEntity entity, UpdatePedidoDto dto)
    {
        entity.Status = dto.Status;
        entity.CodigoPromocionalId = dto.CodigoPromocionalId;
    }
}
