using System.Threading.Tasks;

namespace Donor.Repositories
{
    public interface IDonorRepository
    {
        Task AddDonorAsync(Entities.Donor donor);
        Task<IEnumerable<Entities.Donor>> GetAllDonorsAsync();
		Task<bool> SaveChangesAsync();

	}
}



