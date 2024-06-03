﻿using System.Threading.Tasks;
using Donor.Models;


namespace Donor.Repositories
{
    public interface IDonorRepository
    {
        Task AddDonorAsync(Entities.Donor donor,List<int> Organs);
        Task<IEnumerable<Entities.Donor>> GetAllDonorsAsync();
		Task<bool> SaveChangesAsync();

	}
}



