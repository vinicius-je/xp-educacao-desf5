using PitLaneShop.Model.Entities;
using PitLaneShop.Model.Repositories;
using PitLaneShop.Persistence;

namespace PitLaneShop.Persistence.Repositories;

public class AluguelRepository : BaseRepository<Aluguel>, IAluguelRepository
{
    public AluguelRepository(PitLaneShopDbContext context)
        : base(context)
    {
    }
}
