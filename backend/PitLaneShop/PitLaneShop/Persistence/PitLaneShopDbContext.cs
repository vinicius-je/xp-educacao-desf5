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

    public DbSet<VeiculoModelo> VeiculosModelo => Set<VeiculoModelo>();

    public DbSet<Carro> Carros => Set<Carro>();

    public DbSet<TarifaDiaria> TarifasDiarias => Set<TarifaDiaria>();

    public DbSet<Aluguel> Alugueis => Set<Aluguel>();

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PitLaneShopDbContext).Assembly);
    }
}
