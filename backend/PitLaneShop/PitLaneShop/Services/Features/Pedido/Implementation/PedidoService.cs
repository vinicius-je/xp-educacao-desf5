using PitLaneShop.Model.Repositories;
using PitLaneShop.Services.Abstractions;
using PitLaneShop.Services.Features.Pedido.Dtos;
using PitLaneShop.Services.Features.Pedido.Interfaces;
using PedidoEntity = PitLaneShop.Model.Entities.Pedido;

namespace PitLaneShop.Services.Features.Pedido.Implementation;

public class PedidoService
    : BaseCrudService<PedidoEntity, PedidoResponseDto, CreatePedidoDto, UpdatePedidoDto>, IPedidoService
{
    public PedidoService(IPedidoRepository repository, IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
    }

    protected override PedidoResponseDto MapToResponse(PedidoEntity entity) => new()
    {
        Id = entity.Id,
        DataPedido = entity.DataPedido,
        ValorTotal = entity.ValorTotal,
        Status = entity.Status,
        ClienteId = entity.ClienteId,
        CodigoPromocionalId = entity.CodigoPromocionalId
    };

    protected override PedidoEntity MapFromCreate(CreatePedidoDto dto) =>
        new(dto.DataPedido, dto.ClienteId, dto.CodigoPromocionalId);

    protected override void ApplyUpdate(PedidoEntity entity, UpdatePedidoDto dto)
    {
        entity.Status = dto.Status;
        entity.CodigoPromocionalId = dto.CodigoPromocionalId;
    }
}
