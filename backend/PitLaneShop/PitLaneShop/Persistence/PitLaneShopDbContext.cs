using PitLaneShop.Model.Entities;
using PitLaneShop.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PitLaneShop.Persistence;

public class PitLaneShopDbContext : DbContext, IUnitOfWork
{
    public PitLaneShopDbContext(DbContextOptions<PitLaneShopDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();

    public DbSet<Pedido> Pedidos => Set<Pedido>();

    public DbSet<ItemPedido> ItensPedido => Set<ItemPedido>();

    public DbSet<Produto> Produtos => Set<Produto>();

    public DbSet<CodigoPromocional> CodigosPromocionais => Set<CodigoPromocional>();

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PitLaneShopDbContext).Assembly);
    }
}
