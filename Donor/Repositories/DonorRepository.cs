using Donor.DbContexts;
using Donor.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Linq;

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
            using var transaction = await _context.Database.BeginTransactionAsync();
            try

            {
                await _context.Donors.AddAsync(donor);
                _log.LogInformation("test");
                await _context.SaveChangesAsync();
               await transaction.CommitAsync();

            }

            catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
            {
                _log.LogError(ex, "Database update error while adding donor");
                await transaction.RollbackAsync();
                throw new ValidationException("A donor with the same Identity Number or Email already exists.");
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error adding donor");
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task AddOrgansToDonorAsync(Entities.Donor donor, List<int> organIds)
        {
            foreach (var organId in organIds)
            {
                var organ = await _context.Organs.FindAsync(organId);
                if (organ != null)
                {
                    donor.Organs.Add(organ);
                }
            }
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Entities.Donor>> GetAllDonorsAsync()
        {
            IEnumerable<Entities.Donor> donors = new List<Entities.Donor>();
            try
            {
                donors = await _context.Donors
                    .Include(d => d.Organs)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error getting all donors");
                throw;
            }
            return donors;
        }


        public async Task<Entities.Donor> GetDonorByIdAsync(int id)
        {
            return await _context.Donors.FindAsync(id);
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
