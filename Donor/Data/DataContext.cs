using Microsoft.EntityFrameworkCore;
using Donor.Models;
using System.Collections.Generic;

namespace Donor.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Donor.Models.Donor> Donors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Donor.Models.Donor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.BloodType).IsRequired().HasMaxLength(10);
                entity.Property(e => e.DateOfBirth).IsRequired();
                entity.Property(e => e.ContactNumber).HasMaxLength(15);
                entity.Property(e => e.IdentityNumber).HasMaxLength(50).IsRequired();
                entity.Property(e => e.ResidentialAddress).HasMaxLength(250);
                entity.Property(e => e.MailingAddress).HasMaxLength(250);
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
                entity.Property(e => e.TelephoneNumber).HasMaxLength(15);
                entity.Property(e => e.MobileNumber).HasMaxLength(15);

                entity.HasIndex(e => e.IdentityNumber).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });

        }
    }
}
     

    
