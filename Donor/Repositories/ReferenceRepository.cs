using System.Collections.Generic;
using System.Threading.Tasks;
using Donor.DbContexts;
using Donor.Entities;
using Microsoft.EntityFrameworkCore;

namespace Donor.Repositories
{
    public class ReferenceRepository : IReferenceRepository
    {
        private readonly DonorContext _context;

        public ReferenceRepository(DonorContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Locality>> GetAllLocalitiesAsync()
        {
            return await _context.Localities.ToListAsync();
        }

    public async Task<IEnumerable<Nationality>> GetAllNationalitiesAsync()
        {
            return await _context.Nationalities.ToListAsync();
        }

    }
}