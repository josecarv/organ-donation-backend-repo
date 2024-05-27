using Donor.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Donor.Repositories
{
    public class DonorRepository : IDonorRepository
    {
        private readonly DonorContext _context;
		private readonly ILogger<DonorRepository> _log;

		public DonorRepository(DonorContext context, ILogger<DonorRepository> log)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
			_log = log ?? throw new ArgumentNullException(nameof(log));
		}
	

        public async Task AddDonorAsync(Entities.Donor donor)
        {
			try
			{
				await _context.Donors.AddAsync(donor);				

			}			
			catch (Exception ex)
			{
				_log.LogError(ex, "Error registering donor");
				throw;
			}
			
			
        }

        public async Task<IEnumerable<Entities.Donor>> GetAllDonorsAsync()
        {
			IEnumerable<Entities.Donor > donors = new List<Entities.Donor>();
			try
			{
				donors = await _context.Donors.ToListAsync();
			}
			catch (Exception ex)
			{

				_log.LogError(ex, "Error getting all donors");
				throw;
			}
            return donors;
        }

		public async Task<bool> SaveChangesAsync()
		{
			try
			{
				return await _context.SaveChangesAsync() >= 0;
			}
			catch (Exception ex)
			{

				_log.LogError(ex, "An error occurred while saving changes to the database");
				throw;
			}
		}

		private bool IsUniqueConstraintViolation(DbUpdateException ex)
		{
			if (ex.InnerException is SqlException sqlEx)
			{
				return sqlEx.Number == 2627 || sqlEx.Number == 2601;
			}
			return false;
		}


	}
    }

