using System.Threading.Tasks;
using Donor.Models;

namespace Donor.Repositories
{
    public interface IDonorRepository
    {
        Task AddDonorAsync(Donor.Models.Donor donor);
        Task<IEnumerable<Donor.Models.Donor>> GetAllDonorsAsync();  

    }
}



