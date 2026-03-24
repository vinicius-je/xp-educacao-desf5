using PitLaneShop.Model.Entities;
using PitLaneShop.Model.Repositories;

namespace PitLaneShop.Persistence.Repositories;

public class ItemPedidoRepository : BaseRepository<ItemPedido>, IItemPedidoRepository
{
    public ItemPedidoRepository(PitLaneShopDbContext context)
        : base(context)
    {
    }
}
