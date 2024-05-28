using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Donor.Entities;

namespace Donor.DbContexts
{
    public class DonorContext : DbContext
    {
        public DonorContext(DbContextOptions<DonorContext> options) : base(options) { }

        public DbSet<Entities.Donor> Donors { get; set; }

        public DbSet<DonationPreference> DonationPreferences { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Donor>(entity =>
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
                entity.Property(e => e.PreferredContact)
                    .HasConversion<string>()
                    .HasMaxLength(10); 
                entity.HasIndex(e => e.IdentityNumber).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Organ>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);

                entity.HasData(
                new Organ { Id = 1, Name = "Kidney" },
                new Organ { Id = 2, Name = "Liver" },
                new Organ { Id = 3, Name = "Cartilage" }, 
                new Organ { Id = 4, Name = "Bone Tissue" },
                new Organ { Id = 5, Name = "Small Bowel" },
                new Organ { Id = 6, Name = "Lungs" },
                new Organ { Id = 7, Name = "Heart Valves" },
                new Organ { Id = 8, Name = "Ligaments" },
                new Organ { Id = 9, Name = "Pancreas" },
                new Organ { Id = 10, Name = "Heart" },
                new Organ { Id = 11, Name = "Cornea" },
                new Organ { Id = 12, Name = "Tendons" }
                );
            });

            modelBuilder.Entity<DonationPreference>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Donor)
                      .WithMany(d => d.DonationPreferences)
                      .HasForeignKey(e => e.DonorId);
                entity.HasOne(e => e.Organ)
                      .WithMany()
                      .HasForeignKey(e => e.OrganId);
            });
            
        }
    }
}
     

    
