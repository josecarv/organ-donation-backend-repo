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
            return await _context.Donors
                .Include(d => d.Organs)
                .FirstOrDefaultAsync(d => d.Id == id);
        }




        public async Task UpdateDonorAsync(int donorId, Entities.Donor donorUpdate)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Retrieve the existing donor from the database, including their organs
                var existingDonor = await _context.Donors
                    .Include(d => d.Organs)
                    .FirstOrDefaultAsync(d => d.Id == donorId);

                if (existingDonor == null)
                {
                    throw new ArgumentException("Donor not found with the provided ID");
                }

                existingDonor.FirstName = donorUpdate.FirstName;
                existingDonor.LastName = donorUpdate.LastName;
                existingDonor.BloodGroup = donorUpdate.BloodGroup;
                existingDonor.ResidentialAddress = donorUpdate.ResidentialAddress;
                existingDonor.MailingAddress = donorUpdate.MailingAddress;
                existingDonor.Email = donorUpdate.Email;
                existingDonor.TelephoneNumber = donorUpdate.TelephoneNumber;
                existingDonor.MobileNumber = donorUpdate.MobileNumber;
                existingDonor.Gender = donorUpdate.Gender;

                var updatedOrgans = await _context.Organs
                    .Where(o => donorUpdate.Organs.Select(uo => uo.Id).Contains(o.Id))
                    .ToListAsync();

                existingDonor.Organs = updatedOrgans;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
            {
                _log.LogError(ex, "Database update error while updating donor");
                await transaction.RollbackAsync();
                throw new ValidationException("A donor with the same Identity Number or Email already exists.");
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error updating donor");
                await transaction.RollbackAsync();
                throw;
            }
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
