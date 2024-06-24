using System.Threading.Tasks;
using Donor.Models;


namespace Donor.Repositories
{
    public interface IDonorRepository
    {
        Task AddDonorAsync(Entities.Donor donor);
        Task<IEnumerable<Entities.Donor>> GetAllDonorsAsync();


        Task<IEnumerable<Entities.Organ>> GetAllOrgansAsync();


        Task AddOrgansToDonorAsync(Entities.Donor donor, List<int> organIds);


        Task<Entities.Donor> GetDonorByIdAsync(int id);


        Task UpdateDonorAsync(int donorId, DonorDto DonorDto);


        Task<IEnumerable<Entities.Donor>> SearchDonorsAsync(string searchTerm);



        Task<bool> DonorExistsAsync(int donorId);


        Task<bool> SaveChangesAsync();

    }
}



