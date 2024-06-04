using System.Collections.Generic;
using System.Threading.Tasks;
using Donor.DbContexts;
using Donor.Entities;
using Microsoft.EntityFrameworkCore;

namespace Donor.Repositories
{
    public class LocalityRepository : ILocalityRepository
    {
        private readonly DonorContext _context;

        public LocalityRepository(DonorContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Locality>> GetAllLocalitiesAsync()
        {
            return await _context.Localities.ToListAsync();
        }
    }
}