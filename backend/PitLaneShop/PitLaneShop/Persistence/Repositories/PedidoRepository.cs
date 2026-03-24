using PitLaneShop.Model.Entities;
using PitLaneShop.Model.Repositories;

namespace PitLaneShop.Persistence.Repositories;

public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
{
    public PedidoRepository(PitLaneShopDbContext context)
        : base(context)
    {
    }
}
