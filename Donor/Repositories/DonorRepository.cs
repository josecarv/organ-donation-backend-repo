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
using System.Drawing;
using Donor.Models;
using AutoMapper;

namespace Donor.Repositories
{
    public class DonorRepository : IDonorRepository
    {
        private readonly DonorContext _context;
        private readonly ILogger<DonorRepository> _log;
        private readonly IMapper _mapper;


        public DonorRepository(DonorContext context, IMapper mapper, ILogger<DonorRepository> log)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

        public async Task AddDonorAsync(Entities.Donor donor)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try

            {
                donor.CreatedAt = DateTime.UtcNow;
                donor.UpdatedAt = DateTime.UtcNow;


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
            await _context.Entry(donor).Collection(d => d.Organs).LoadAsync();

            var existingOrgans = await _context.Organs
                .Where(o => organIds.Contains(o.Id))
                .ToListAsync();

            donor.Organs.Clear();
            foreach (var organ in existingOrgans)
            {
                donor.Organs.Add(organ);
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





        public async Task<IEnumerable<Entities.Organ>> GetAllOrgansAsync()
        {
            IEnumerable<Entities.Organ> organs = new List<Entities.Organ>();
            try
            {
                organs = await _context.Organs
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error getting all donors");
                throw;
            }
            return organs;
        }





        public async Task<Entities.Donor> GetDonorByIdAsync(int id)
        {
            return await _context.Donors
                .Include(d => d.Organs)
                .FirstOrDefaultAsync(d => d.Id == id);
        }




        public async Task UpdateDonorAsync(int donorId, Entities.Donor donorUpdate)
        {
            try
            {
                var entity = await _context.Donors
                 //  .Include(d => d.Organs)
                   .FirstOrDefaultAsync(d => d.Id == donorId);

                /**   _context.Entry(existingDonor).CurrentValues.SetValues(donorUpdate);

                   var updatedOrgans = await _context.Organs
                       .Where(o => donorUpdate.Organs.Select(uo => uo.Id).Contains(o.Id))
                       .ToListAsync();

                   existingDonor.ResidentialAddress = donorUpdate.ResidentialAddress;
                   existingDonor.MailingAddress = donorUpdate.MailingAddress;
                   existingDonor.Organs = updatedOrgans;
                   existingDonor.UpdatedAt = DateTime.UtcNow; **/

                _mapper.Map(donorUpdate, entity);

                _context.Donors.Update(entity);


            }
            catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex)) 
            {
                _log.LogError(ex, "Database update error while updating donor");
                throw new ValidationException("A donor with the same Identity Number or Email already exists.");
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error updating donor");
                throw;
            }
        }



   /**     public async Task<IEnumerable<Entities.Donor>> SearchDonorsAsync(string searchTerm)
        {
            return await _context.Donors
                .Where(d => d.FirstName.Contains(searchTerm) || d.LastName.Contains(searchTerm) || d.Nationality.Contains(searchTerm) ||
                         d.Gender.Contains(searchTerm)   || d.IdentityNumber.Contains(searchTerm))
                .Include(d => d.Organs)
                .ToListAsync();
        }**/

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


        public async Task<bool> DonorExistsAsync(int donorId)
        {
            return await _context.Donors.AnyAsync(d => d.Id == donorId);
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
