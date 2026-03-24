using PitLaneShop.Model.Entities;
using PitLaneShop.Model.Repositories;

namespace PitLaneShop.Persistence.Repositories;

public class CodigoPromocionalRepository : BaseRepository<CodigoPromocional>, ICodigoPromocionalRepository
{
    public CodigoPromocionalRepository(PitLaneShopDbContext context)
        : base(context)
    {
    }
}
