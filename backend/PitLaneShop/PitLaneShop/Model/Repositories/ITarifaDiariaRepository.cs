using PitLaneShop.Model.Entities;

namespace PitLaneShop.Model.Repositories;

public interface ITarifaDiariaRepository : IBaseRepository<TarifaDiaria>
{
    Task<TarifaDiaria?> GetVigenteByModeloAsync(Guid veiculoModeloId, CancellationToken cancellationToken = default);
}
