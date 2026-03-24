using PitLaneShop.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PitLaneShop.Persistence.EntitiesMapping;

public class CodigoPromocionalConfiguration : IEntityTypeConfiguration<CodigoPromocional>
{
    public void Configure(EntityTypeBuilder<CodigoPromocional> builder)
    {
        builder.ToTable("CodigosPromocionais");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Codigo).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Desconto).HasPrecision(18, 2);

        builder.HasIndex(c => c.Codigo).IsUnique();

        builder.HasMany(c => c.Pedidos)
            .WithOne(p => p.CodigoPromocional)
            .HasForeignKey(p => p.CodigoPromocionalId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
