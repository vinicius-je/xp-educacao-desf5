using PitLaneShop.Model.Repositories;
using PitLaneShop.Services.Abstractions;
using PitLaneShop.Services.Features.Carro.Dtos;
using PitLaneShop.Services.Features.Carro.Interfaces;
using CarroEntity = PitLaneShop.Model.Entities.Carro;

namespace PitLaneShop.Services.Features.Carro.Implementation;

public class CarroService
    : BaseCrudService<CarroEntity, CarroResponseDto, CreateCarroDto, UpdateCarroDto>, ICarroService
{
    public CarroService(ICarroRepository repository, IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
    }

    protected override CarroResponseDto MapToResponse(CarroEntity entity) => new()
    {
        Id = entity.Id,
        Placa = entity.Placa,
        Quilometragem = entity.Quilometragem,
        Status = entity.Status,
        Imagem = entity.Imagem,
        VeiculoModeloId = entity.VeiculoModeloId,
        DataCriacao = entity.DataCriacao,
        DataAtualizacao = entity.DataAtualizacao
    };

    protected override CarroEntity MapFromCreate(CreateCarroDto dto) =>
        new(dto.Placa, dto.Quilometragem, dto.Status, dto.VeiculoModeloId, dto.Imagem);

    protected override void ApplyUpdate(CarroEntity entity, UpdateCarroDto dto)
    {
        entity.Placa = dto.Placa;
        entity.Quilometragem = dto.Quilometragem;
        entity.Status = dto.Status;
        entity.Imagem = dto.Imagem;
        entity.VeiculoModeloId = dto.VeiculoModeloId;
    }
}
