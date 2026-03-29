using AutoMapper;
using PitLaneShop.Model.Repositories;
using PitLaneShop.Services.Abstractions;
using PitLaneShop.Services.Features.Pedido.Dtos;
using PitLaneShop.Services.Features.Pedido.Interfaces;
using PedidoEntity = PitLaneShop.Model.Entities.Pedido;

namespace PitLaneShop.Services.Features.Pedido.Implementation;

public class PedidoService
    : BaseCrudService<PedidoEntity, PedidoResponseDto, CreatePedidoDto, UpdatePedidoDto>, IPedidoService
{
    private readonly IPedidoItemBuilder _pedidoItemBuilder;
    private readonly ICodigoPromocionalRepository _codigoPromocionalRepository;

    public PedidoService(
        IPedidoRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICodigoPromocionalRepository codigoPromocionalRepository,
        IPedidoItemBuilder pedidoItemBuilder
        ) : base(repository, unitOfWork, mapper)
    {
        _codigoPromocionalRepository = codigoPromocionalRepository;
        _pedidoItemBuilder = pedidoItemBuilder;
    }

    public override async Task<PedidoResponseDto> CreateAsync(
        CreatePedidoDto dto,
        CancellationToken cancellationToken = default)
    {
        var pedido = new PedidoEntity(DateOnly.FromDateTime(DateTime.UtcNow), dto.ClienteId, dto.CodigoPromocionalId);
        await _pedidoItemBuilder.AdicionarItensNoPedidoAsync(dto, pedido, cancellationToken);
        var percentualDesconto = await _codigoPromocionalRepository.ObterPercentualDescontoAsync(dto.CodigoPromocionalId, cancellationToken);
       
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

    public async Task<IEnumerable<PedidoResponseDto>> GetPedidosPorClienteIdAsync(Guid clienteId, CancellationToken cancellationToken)
    {
        var pedidos = await ((IPedidoRepository)Repository).GetPedidosPorClienteIdAsync(clienteId, cancellationToken);
        return Mapper.Map<IEnumerable<PedidoResponseDto>>(pedidos);
    }
}
