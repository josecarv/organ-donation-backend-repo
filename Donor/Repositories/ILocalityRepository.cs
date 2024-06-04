using System.Collections.Generic;
using System.Threading.Tasks;
using Donor.Entities;

namespace Donor.Repositories
{
    public interface ILocalityRepository
    {
        Task<IEnumerable<Locality>> GetAllLocalitiesAsync();
    }
}
