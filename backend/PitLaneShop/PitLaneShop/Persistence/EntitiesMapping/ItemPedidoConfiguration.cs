using PitLaneShop.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PitLaneShop.Persistence.EntitiesMapping;

public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
{
    public void Configure(EntityTypeBuilder<ItemPedido> builder)
    {
        builder.ToTable("ItensPedido");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.ValorUnitario).HasPrecision(18, 2);
        builder.Property(i => i.ValorTotal).HasPrecision(18, 2);
        builder.Property(i => i.Descricao).HasMaxLength(500);
    }
}
