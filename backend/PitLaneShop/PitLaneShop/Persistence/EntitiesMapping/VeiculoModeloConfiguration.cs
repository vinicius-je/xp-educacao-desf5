using PitLaneShop.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PitLaneShop.Persistence.EntitiesMapping;

public class VeiculoModeloConfiguration : IEntityTypeConfiguration<VeiculoModelo>
{
    public void Configure(EntityTypeBuilder<VeiculoModelo> builder)
    {
        builder.ToTable("VeiculosModelo");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Marca).IsRequired().HasMaxLength(120);
        builder.Property(v => v.Modelo).IsRequired().HasMaxLength(120);
        builder.Property(v => v.Categoria).HasConversion<int>();

        builder.HasMany(v => v.Carros)
            .WithOne(c => c.VeiculoModelo)
            .HasForeignKey(c => c.VeiculoModeloId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(v => v.TarifasDiarias)
            .WithOne(t => t.VeiculoModelo)
            .HasForeignKey(t => t.VeiculoModeloId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
