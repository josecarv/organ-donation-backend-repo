using System.Collections.Generic;
using System.Threading.Tasks;
using Donor.Entities;

namespace Donor.Repositories
{
    public interface IReferenceRepository
    {
        Task<IEnumerable<Locality>> GetAllLocalitiesAsync();


         Task<IEnumerable<Nationality>> GetAllNationalitiesAsync();


        
    }
}
