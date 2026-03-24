using PitLaneShop.Model.Repositories;
using PitLaneShop.Services.Abstractions;
using PitLaneShop.Services.Features.CodigoPromocional.Dtos;
using PitLaneShop.Services.Features.CodigoPromocional.Interfaces;
using CodigoPromocionalEntity = PitLaneShop.Model.Entities.CodigoPromocional;

namespace PitLaneShop.Services.Features.CodigoPromocional.Implementation;

public class CodigoPromocionalService
    : BaseCrudService<CodigoPromocionalEntity, CodigoPromocionalResponseDto, CreateCodigoPromocionalDto, UpdateCodigoPromocionalDto>,
      ICodigoPromocionalService
{
    public CodigoPromocionalService(ICodigoPromocionalRepository repository, IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
    }

    protected override CodigoPromocionalResponseDto MapToResponse(CodigoPromocionalEntity entity) => new()
    {
        Id = entity.Id,
        Codigo = entity.Codigo,
        Desconto = entity.Desconto,
        EhValido = entity.EhValido
    };

    protected override CodigoPromocionalEntity MapFromCreate(CreateCodigoPromocionalDto dto) =>
        new(dto.Codigo, dto.Desconto, dto.EhValido);

    protected override void ApplyUpdate(CodigoPromocionalEntity entity, UpdateCodigoPromocionalDto dto)
    {
        entity.Codigo = dto.Codigo;
        entity.Desconto = dto.Desconto;
        entity.EhValido = dto.EhValido;
    }
}
