using PitLaneShop.Model.Entities;

namespace PitLaneShop.Model.Repositories;

public interface IClienteRepository : IBaseRepository<Cliente>
{
    Task<Cliente?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
