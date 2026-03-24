using PitLaneShop.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PitLaneShop.Persistence.EntitiesMapping;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Imagem).HasMaxLength(500);
        builder.Property(p => p.Descricao).HasMaxLength(1000);
        builder.Property(p => p.Preco).HasPrecision(18, 2);
        builder.Property(p => p.Categoria).IsRequired();

        builder.HasMany(p => p.ItensPedido)
            .WithOne(ip => ip.Produto)
            .HasForeignKey(ip => ip.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
