using PitLaneShop.Model.Entities;
using PitLaneShop.Model.Repositories;

namespace PitLaneShop.Persistence.Repositories;

public class CodigoPromocionalRepository : BaseRepository<CodigoPromocional>, ICodigoPromocionalRepository
{
    public CodigoPromocionalRepository(PitLaneShopDbContext context)
        : base(context)
    {
    }

    public async Task<decimal> ObterPercentualDescontoAsync(Guid? codigoPromocionalId, CancellationToken cancellationToken)
    {
        if (!codigoPromocionalId.HasValue)
            return 0;

        var codigo = await this.GetByIdAsync(codigoPromocionalId.Value, cancellationToken);

        return codigo is not null && codigo.EhValido ? codigo.Desconto : 0;
    }
}
