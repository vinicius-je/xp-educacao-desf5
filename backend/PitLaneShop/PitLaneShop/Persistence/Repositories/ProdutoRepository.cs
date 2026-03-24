using PitLaneShop.Model.Entities;
using PitLaneShop.Model.Repositories;

namespace PitLaneShop.Persistence.Repositories;

public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
{
    public ProdutoRepository(PitLaneShopDbContext context)
        : base(context)
    {
    }
}
