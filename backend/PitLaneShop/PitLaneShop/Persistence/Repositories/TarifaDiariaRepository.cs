using PitLaneShop.Model.Entities;
using PitLaneShop.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PitLaneShop.Persistence.Repositories;

public class TarifaDiariaRepository : BaseRepository<TarifaDiaria>, ITarifaDiariaRepository
{
    public TarifaDiariaRepository(PitLaneShopDbContext context)
        : base(context)
    {
    }

    public async Task<TarifaDiaria?> GetVigenteByModeloAsync(
        Guid veiculoModeloId, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(t =>
            t.VeiculoModeloId == veiculoModeloId &&
            t.EhValorDiariaVigente, cancellationToken);
    }
}
