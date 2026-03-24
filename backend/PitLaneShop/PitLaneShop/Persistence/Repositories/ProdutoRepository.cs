using PitLaneShop.Model.Entities;
using PitLaneShop.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PitLaneShop.Persistence.Repositories;

public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
{
    public ProdutoRepository(PitLaneShopDbContext context)
        : base(context)
    {
    }

    public async Task<List<Produto>> GetByNomeAsync(string nome, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(p => EF.Functions.Like(p.Nome, $"%{nome}%"))
            .ToListAsync(cancellationToken);
    }
}
