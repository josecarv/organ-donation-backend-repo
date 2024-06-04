using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Donor.Entities;

namespace Donor.DbContexts
{
    public class DonorContext : DbContext
    {
        public DonorContext(DbContextOptions<DonorContext> options) : base(options) { }

        public DbSet<Entities.Donor> Donors { get; set; }

        public DbSet<Organ> Organs { get; set; }
        

        public DbSet<Locality> Localities { get; set; } 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Donor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.BloodGroup).IsRequired().HasMaxLength(10);
                entity.Property(e => e.DateOfBirth).IsRequired();
                entity.Property(e => e.IdentityNumber).HasMaxLength(50).IsRequired();

                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
                entity.Property(e => e.TelephoneNumber).HasMaxLength(15);
                entity.Property(e => e.MobileNumber).HasMaxLength(15);
                entity.Property(e => e.PreferredContact)
                    .HasConversion<string>()
                    .HasMaxLength(10);

                entity.OwnsOne(e => e.ResidentialAddress, addr =>
               {
                   addr.Property(a => a.Street).HasMaxLength(100);
                   addr.Property(a => a.Locality).HasMaxLength(100);
                   addr.Property(a => a.PostCode).HasMaxLength(20);
               });

                entity.OwnsOne(e => e.MailingAddress, addr =>
                {
                    addr.Property(a => a.Street).HasMaxLength(100);
                    addr.Property(a => a.Locality).HasMaxLength(100);
                    addr.Property(a => a.PostCode).HasMaxLength(20);
                });

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

            modelBuilder.Entity<Entities.Donor>()
                 .HasMany(d => d.Organs)
                 .WithMany(o => o.Donors)
                 .UsingEntity<Dictionary<string, object>>(
                     "DonorOrgan",
                     j => j
                         .HasOne<Organ>()
                         .WithMany()
                         .HasForeignKey("OrganId")
                         .OnDelete(DeleteBehavior.Cascade),
                     j => j
                         .HasOne<Entities.Donor>()
                         .WithMany()
                         .HasForeignKey("DonorId")
                         .OnDelete(DeleteBehavior.Cascade),
                     j =>
                     {
                         j.HasKey("DonorId", "OrganId");
                     });



            modelBuilder.Entity<Locality>(entity =>
{
    entity.HasKey(e => e.Id);
    entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

    entity.HasData(
        new Locality { Id = 1, Name = "Bugibba" },
        new Locality { Id = 2, Name = "Bahar ic-Caghaq" },
        new Locality { Id = 3, Name = "Gharghur" },
        new Locality { Id = 4, Name = "Mellieha" },
        new Locality { Id = 5, Name = "Manikata" },
        new Locality { Id = 6, Name = "Mgarr" },
        new Locality { Id = 7, Name = "St Paul's Bay" },
        new Locality { Id = 8, Name = "Qawra" },
        new Locality { Id = 9, Name = "Attard" },
        new Locality { Id = 10, Name = "Balzan" },
        new Locality { Id = 11, Name = "Birkirkara" },
        new Locality { Id = 12, Name = "Dingli" },
        new Locality { Id = 13, Name = "Iklin" },
        new Locality { Id = 14, Name = "Lija" },
        new Locality { Id = 15, Name = "Mdina" },
        new Locality { Id = 16, Name = "Mosta" },
        new Locality { Id = 17, Name = "Mtarfa" },
        new Locality { Id = 18, Name = "Naxxar" },
        new Locality { Id = 19, Name = "Qormi" },
        new Locality { Id = 20, Name = "Rabat" },
        new Locality { Id = 21, Name = "Siggiewi" },
        new Locality { Id = 22, Name = "Zebbug" },
        new Locality { Id = 23, Name = "Gzira" },
        new Locality { Id = 24, Name = "Hamrun" },
        new Locality { Id = 25, Name = "Msida" },
        new Locality { Id = 26, Name = "Pembroke" },
        new Locality { Id = 27, Name = "Pieta" },
        new Locality { Id = 28, Name = "Kappara" },
        new Locality { Id = 29, Name = "Paceville" },
        new Locality { Id = 30, Name = "St Julian's" },
        new Locality { Id = 31, Name = "San Gwann" },
        new Locality { Id = 32, Name = "Santa Venera" },
        new Locality { Id = 33, Name = "Sliema" },
        new Locality { Id = 34, Name = "Swieqi" },
        new Locality { Id = 35, Name = "Ta' Xbiex" },
        new Locality { Id = 36, Name = "Birgu (Vittoriosa)" },
        new Locality { Id = 37, Name = "Bormla (Cospicua)" },
        new Locality { Id = 38, Name = "Fgura" },
        new Locality { Id = 39, Name = "Floriana" },
        new Locality { Id = 40, Name = "Isla (Senglea)" },
        new Locality { Id = 41, Name = "Kalkara" },
        new Locality { Id = 42, Name = "Luqa" },
        new Locality { Id = 43, Name = "Marsa" },
        new Locality { Id = 44, Name = "Paola" },
        new Locality { Id = 45, Name = "Santa Lucija" },
        new Locality { Id = 46, Name = "Tarxien" },
        new Locality { Id = 47, Name = "Valletta" },
        new Locality { Id = 48, Name = "Xghajra" },
        new Locality { Id = 49, Name = "Zabbar" },
        new Locality { Id = 50, Name = "Birzebbuga" },
        new Locality { Id = 51, Name = "Ghaxaq" },
        new Locality { Id = 52, Name = "Gudja" },
        new Locality { Id = 53, Name = "Kirkop" },
        new Locality { Id = 54, Name = "Marsascala" },
        new Locality { Id = 55, Name = "Marsaxlokk" },
        new Locality { Id = 56, Name = "Mqabba" },
        new Locality { Id = 57, Name = "Qrendi" },
        new Locality { Id = 58, Name = "Safi" },
        new Locality { Id = 59, Name = "Zejtun" },
        new Locality { Id = 60, Name = "Zurrieq" }
    );
});

        }
    }
}



