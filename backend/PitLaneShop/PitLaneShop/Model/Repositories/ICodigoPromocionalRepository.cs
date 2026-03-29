using PitLaneShop.Model.Entities;

namespace PitLaneShop.Model.Repositories;

public interface ICodigoPromocionalRepository : IBaseRepository<CodigoPromocional>
{
    Task<decimal> ObterPercentualDescontoAsync(Guid? codigoPromocionalId, CancellationToken cancellationToken);
}
