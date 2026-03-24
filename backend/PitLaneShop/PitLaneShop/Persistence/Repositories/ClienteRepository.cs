using PitLaneShop.Model.Entities;
using PitLaneShop.Model.Repositories;
using PitLaneShop.Persistence;

namespace PitLaneShop.Persistence.Repositories;

public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
{
    public ClienteRepository(PitLaneShopDbContext context)
        : base(context)
    {
    }
}
