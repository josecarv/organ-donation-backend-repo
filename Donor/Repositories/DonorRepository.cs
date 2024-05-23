using Donor.Data;
using Donor.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Donor.Repositories
{
    public class DonorRepository : IDonorRepository
    {
        private readonly DataContext _context;

        public DonorRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddDonorAsync(Donor.Models.Donor donor)
        {
            await _context.Donors.AddAsync(donor);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Donor.Models.Donor>> GetAllDonorsAsync()
        {
            return await _context.Donors.ToListAsync();  
        }

    
      }
    }

