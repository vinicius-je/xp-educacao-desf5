using PitLaneShop.Model.Entities;

namespace PitLaneShop.Model.Repositories;

public interface ICarroRepository : IBaseRepository<Carro>
{
    Task<Carro?> GetFirstDisponivelByModeloAsync(Guid veiculoModeloId, CancellationToken cancellationToken = default);
}
