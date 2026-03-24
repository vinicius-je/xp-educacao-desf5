namespace PitLaneShop.Model.Repositories;

public interface IUnitOfWork
{
    Task SaveAsync(CancellationToken cancellationToken = default);
}
