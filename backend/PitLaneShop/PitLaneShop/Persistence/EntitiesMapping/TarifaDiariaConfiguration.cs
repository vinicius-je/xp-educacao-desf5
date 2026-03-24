using PitLaneShop.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PitLaneShop.Persistence.EntitiesMapping;

public class TarifaDiariaConfiguration : IEntityTypeConfiguration<TarifaDiaria>
{
    public void Configure(EntityTypeBuilder<TarifaDiaria> builder)
    {
        builder.ToTable("TarifasDiarias");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.ValorDiaria).HasPrecision(18, 2);
        builder.Property(t => t.ValorMulta).HasPrecision(18, 2);
        builder.Property(t => t.DataInicioVigencia);
        builder.Property(t => t.DataFimVigencia);
    }
}
