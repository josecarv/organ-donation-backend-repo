using System.Threading.Tasks;
using Donor.Models;


namespace Donor.Repositories
{
    public interface IDonorRepository
    {
        Task AddDonorAsync(Entities.Donor donor);
        Task<IEnumerable<Entities.Donor>> GetAllDonorsAsync();

        Task AddOrgansToDonorAsync(Entities.Donor donor, List<int> organIds);


        Task<Entities.Donor> GetDonorByIdAsync(int id);


        Task UpdateDonorAsync(int donorId, Entities.Donor donorUpdate);


        Task<bool> SaveChangesAsync();

    }
}



