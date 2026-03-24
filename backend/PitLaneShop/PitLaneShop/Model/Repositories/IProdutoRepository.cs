using PitLaneShop.Model.Entities;

namespace PitLaneShop.Model.Repositories;

public interface IProdutoRepository : IBaseRepository<Produto>
{
    Task<List<Produto>> GetByNomeAsync(string nome, CancellationToken cancellationToken = default);
}
