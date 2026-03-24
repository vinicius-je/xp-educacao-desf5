using PitLaneShop.Model.Entities;
using PitLaneShop.Model.Enums;
using PitLaneShop.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PitLaneShop.Persistence.Repositories;

public class CarroRepository : BaseRepository<Carro>, ICarroRepository
{
    public CarroRepository(PitLaneShopDbContext context)
        : base(context)
    {
    }

    public async Task<Carro?> GetFirstDisponivelByModeloAsync(
        Guid veiculoModeloId, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(c =>
            c.VeiculoModeloId == veiculoModeloId &&
            c.Status == StatusCarro.Disponivel, cancellationToken);
    }
}
