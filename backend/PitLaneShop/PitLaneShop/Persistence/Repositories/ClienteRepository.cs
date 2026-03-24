using PitLaneShop.Model.Entities;
using PitLaneShop.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PitLaneShop.Persistence.Repositories;

public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
{
    public ClienteRepository(PitLaneShopDbContext context)
        : base(context)
    {
    }

    public async Task<Cliente?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
    }
}
