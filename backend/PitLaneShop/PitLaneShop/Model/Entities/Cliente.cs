namespace PitLaneShop.Model.Entities;

public class Cliente : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;

    public ICollection<Aluguel> Alugueis { get; set; } = new List<Aluguel>();

    public Cliente()
    {
    }

    public Cliente(string nome, string email, string telefone)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }
}
