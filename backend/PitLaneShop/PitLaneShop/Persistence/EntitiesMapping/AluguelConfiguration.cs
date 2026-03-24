using PitLaneShop.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PitLaneShop.Persistence.EntitiesMapping;

public class AluguelConfiguration : IEntityTypeConfiguration<Aluguel>
{
    public void Configure(EntityTypeBuilder<Aluguel> builder)
    {
        builder.ToTable("Alugueis");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.DataRetirada);
        builder.Property(a => a.DataDevolucaoPrevista);
        builder.Property(a => a.DataDevolucao);
        builder.Property(a => a.ValorTotal).HasPrecision(18, 2);
        builder.Property(a => a.ValorMulta).HasPrecision(18, 2);
    }
}
