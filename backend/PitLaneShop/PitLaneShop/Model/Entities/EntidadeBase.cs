namespace PitLaneShop.Model.Entities;

public abstract class EntidadeBase
{
    public Guid Id { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime? DataAtualizacao { get; set; }

    protected EntidadeBase()
    {
        Id = Guid.NewGuid();
        DataCriacao = DateTime.UtcNow;
    }

    public void AtualizarDataAtualizacao()
    {
        DataAtualizacao = DateTime.UtcNow;
    }
}
