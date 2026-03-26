using PitLaneShop.Model.Entities;
using PitLaneShop.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PitLaneShop.Persistence.Repositories;

public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
{
    public PedidoRepository(PitLaneShopDbContext context)
        : base(context)
    {
    }

    public override async Task<Pedido?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public override async Task<List<Pedido>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(p => p.Itens)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Pedido>> GetPedidosPorClienteIdAsync(Guid clienteId, CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(p => p.ClienteId == clienteId)
            .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
