using PitLaneShop.Model.Entities;

namespace PitLaneShop.Model.Repositories;

public interface IPedidoRepository : IBaseRepository<Pedido>
{
    Task<IEnumerable<Pedido>> GetPedidosPorClienteIdAsync(Guid clienteId, CancellationToken cancellationToken);
}
