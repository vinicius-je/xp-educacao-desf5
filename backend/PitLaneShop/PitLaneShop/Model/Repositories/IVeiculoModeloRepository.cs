using PitLaneShop.Model.Entities;

namespace PitLaneShop.Model.Repositories;

public interface IVeiculoModeloRepository : IBaseRepository<VeiculoModelo>
{
    IQueryable<VeiculoModelo> GetAllWithCarrosAndTarifas();
}
