using PitLaneShop.Model.Entities;
using PitLaneShop.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PitLaneShop.Persistence.Repositories;

public class VeiculoModeloRepository : BaseRepository<VeiculoModelo>, IVeiculoModeloRepository
{
    public VeiculoModeloRepository(PitLaneShopDbContext context)
        : base(context)
    {
    }

    public IQueryable<VeiculoModelo> GetAllWithCarrosAndTarifas()
    {
        return DbSet.AsNoTracking()
            .Include(v => v.Carros)
            .Include(v => v.TarifasDiarias.Where(t => t.EhValorDiariaVigente));
    }
}
