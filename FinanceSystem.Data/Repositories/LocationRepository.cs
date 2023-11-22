using FinanceSystem.Data.Entities;
using FinanceSystem.Data.Interfaces.Locations;
using FinanceSystem.Data.Repositories.Base;

namespace FinanceSystem.Data.Repositories;

public sealed class LocationRepository : BaseRepository<Location>, ILocationRepository
{
    public LocationRepository(IFinanceSystemDbContext context) : base(context)
    {
    }
}