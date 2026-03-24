using PitLaneShop.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PitLaneShop.Persistence.EntitiesMapping;

public class CarroConfiguration : IEntityTypeConfiguration<Carro>
{
    public void Configure(EntityTypeBuilder<Carro> builder)
    {
        builder.ToTable("Carros");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Placa).IsRequired().HasMaxLength(20);
        builder.Property(c => c.Quilometragem).IsRequired().HasMaxLength(32);
        builder.Property(c => c.Status).HasConversion<int>();
        builder.Property(c => c.Imagem).IsRequired().HasMaxLength(255);

        builder.HasIndex(c => c.Placa).IsUnique();

        builder.HasMany(c => c.Alugueis)
            .WithOne(a => a.Carro)
            .HasForeignKey(a => a.CarroId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
