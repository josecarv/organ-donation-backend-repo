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


        public DbSet<Nationality> Nationalities { get; set; }



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
            modelBuilder.Entity<Nationality>(entity =>
                    {
                        entity.HasKey(e => e.Id);
                        entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

                        entity.HasData(
                            new Nationality { Id = 1, Name = "Afghan" },
                            new Nationality { Id = 2, Name = "Albanian" },
                            new Nationality { Id = 3, Name = "Algerian" },
                            new Nationality { Id = 4, Name = "American" },
                            new Nationality { Id = 5, Name = "Andorran" },
                            new Nationality { Id = 6, Name = "Angolan" },
                            new Nationality { Id = 7, Name = "Antiguans" },
                            new Nationality { Id = 8, Name = "Argentinean" },
                            new Nationality { Id = 9, Name = "Armenian" },
                            new Nationality { Id = 10, Name = "Australian" },
                            new Nationality { Id = 11, Name = "Austrian" },
                            new Nationality { Id = 12, Name = "Azerbaijani" },
                            new Nationality { Id = 13, Name = "Bahamian" },
                            new Nationality { Id = 14, Name = "Bahraini" },
                            new Nationality { Id = 15, Name = "Bangladeshi" },
                            new Nationality { Id = 16, Name = "Barbadian" },
                            new Nationality { Id = 17, Name = "Barbudans" },
                            new Nationality { Id = 18, Name = "Batswana" },
                            new Nationality { Id = 19, Name = "Belarusian" },
                            new Nationality { Id = 20, Name = "Belgian" },
                            new Nationality { Id = 21, Name = "Belizean" },
                            new Nationality { Id = 22, Name = "Beninese" },
                            new Nationality { Id = 23, Name = "Bhutanese" },
                            new Nationality { Id = 24, Name = "Bolivian" },
                            new Nationality { Id = 25, Name = "Bosnian" },
                            new Nationality { Id = 26, Name = "Brazilian" },
                            new Nationality { Id = 27, Name = "British" },
                            new Nationality { Id = 28, Name = "Bruneian" },
                            new Nationality { Id = 29, Name = "Bulgarian" },
                            new Nationality { Id = 30, Name = "Burkinabe" },
                            new Nationality { Id = 31, Name = "Burmese" },
                            new Nationality { Id = 32, Name = "Burundian" },
                            new Nationality { Id = 33, Name = "Cambodian" },
                            new Nationality { Id = 34, Name = "Cameroonian" },
                            new Nationality { Id = 35, Name = "Canadian" },
                            new Nationality { Id = 36, Name = "Cape Verdean" },
                            new Nationality { Id = 37, Name = "Central African" },
                            new Nationality { Id = 38, Name = "Chadian" },
                            new Nationality { Id = 39, Name = "Chilean" },
                            new Nationality { Id = 40, Name = "Chinese" },
                            new Nationality { Id = 41, Name = "Colombian" },
                            new Nationality { Id = 42, Name = "Comoran" },
                            new Nationality { Id = 43, Name = "Congolese" },
                            new Nationality { Id = 44, Name = "Costa Rican" },
                            new Nationality { Id = 45, Name = "Croatian" },
                            new Nationality { Id = 46, Name = "Cuban" },
                            new Nationality { Id = 47, Name = "Cypriot" },
                            new Nationality { Id = 48, Name = "Czech" },
                            new Nationality { Id = 49, Name = "Danish" },
                            new Nationality { Id = 50, Name = "Djibouti" },
                            new Nationality { Id = 51, Name = "Dominican" },
                            new Nationality { Id = 52, Name = "Dutch" },
                            new Nationality { Id = 53, Name = "East Timorese" },
                            new Nationality { Id = 54, Name = "Ecuadorean" },
                            new Nationality { Id = 55, Name = "Egyptian" },
                            new Nationality { Id = 56, Name = "Emirian" },
                            new Nationality { Id = 57, Name = "Equatorial Guinean" },
                            new Nationality { Id = 58, Name = "Eritrean" },
                            new Nationality { Id = 59, Name = "Estonian" },
                            new Nationality { Id = 60, Name = "Ethiopian" },
                            new Nationality { Id = 61, Name = "Fijian" },
                            new Nationality { Id = 62, Name = "Filipino" },
                            new Nationality { Id = 63, Name = "Finnish" },
                            new Nationality { Id = 64, Name = "French" },
                            new Nationality { Id = 65, Name = "Gabonese" },
                            new Nationality { Id = 66, Name = "Gambian" },
                            new Nationality { Id = 67, Name = "Georgian" },
                            new Nationality { Id = 68, Name = "German" },
                            new Nationality { Id = 69, Name = "Ghanaian" },
                            new Nationality { Id = 70, Name = "Greek" },
                            new Nationality { Id = 71, Name = "Grenadian" },
                            new Nationality { Id = 72, Name = "Guatemalan" },
                            new Nationality { Id = 73, Name = "Guinea-Bissauan" },
                            new Nationality { Id = 74, Name = "Guinean" },
                            new Nationality { Id = 75, Name = "Guyanese" },
                            new Nationality { Id = 76, Name = "Haitian" },
                            new Nationality { Id = 77, Name = "Herzegovinian" },
                            new Nationality { Id = 78, Name = "Honduran" },
                            new Nationality { Id = 79, Name = "Hungarian" },
                            new Nationality { Id = 80, Name = "I-Kiribati" },
                            new Nationality { Id = 81, Name = "Icelander" },
                            new Nationality { Id = 82, Name = "Indian" },
                            new Nationality { Id = 83, Name = "Indonesian" },
                            new Nationality { Id = 84, Name = "Iranian" },
                            new Nationality { Id = 85, Name = "Iraqi" },
                            new Nationality { Id = 86, Name = "Irish" },
                            new Nationality { Id = 87, Name = "Israeli" },
                            new Nationality { Id = 88, Name = "Italian" },
                            new Nationality { Id = 89, Name = "Ivorian" },
                            new Nationality { Id = 90, Name = "Jamaican" },
                            new Nationality { Id = 91, Name = "Japanese" },
                            new Nationality { Id = 92, Name = "Jordanian" },
                            new Nationality { Id = 93, Name = "Kazakhstani" },
                            new Nationality { Id = 94, Name = "Kenyan" },
                            new Nationality { Id = 95, Name = "Kittian and Nevisian" },
                            new Nationality { Id = 96, Name = "Kuwaiti" },
                            new Nationality { Id = 97, Name = "Kyrgyz" },
                            new Nationality { Id = 98, Name = "Laotian" },
                            new Nationality { Id = 99, Name = "Latvian" },
                            new Nationality { Id = 100, Name = "Lebanese" },
                            new Nationality { Id = 101, Name = "Liberian" },
                            new Nationality { Id = 102, Name = "Libyan" },
                            new Nationality { Id = 103, Name = "Liechtensteiner" },
                            new Nationality { Id = 104, Name = "Lithuanian" },
                            new Nationality { Id = 105, Name = "Luxembourger" },
                            new Nationality { Id = 106, Name = "Macedonian" },
                            new Nationality { Id = 107, Name = "Malagasy" },
                            new Nationality { Id = 108, Name = "Malawian" },
                            new Nationality { Id = 109, Name = "Malaysian" },
                            new Nationality { Id = 110, Name = "Maldivan" },
                            new Nationality { Id = 111, Name = "Malian" },
                            new Nationality { Id = 112, Name = "Maltese" },
                            new Nationality { Id = 113, Name = "Marshallese" },
                            new Nationality { Id = 114, Name = "Mauritanian" },
                            new Nationality { Id = 115, Name = "Mauritian" },
                            new Nationality { Id = 116, Name = "Mexican" },
                            new Nationality { Id = 117, Name = "Micronesian" },
                            new Nationality { Id = 118, Name = "Moldovan" },
                            new Nationality { Id = 119, Name = "Monacan" },
                            new Nationality { Id = 120, Name = "Mongolian" },
                            new Nationality { Id = 121, Name = "Moroccan" },
                            new Nationality { Id = 122, Name = "Mosotho" },
                            new Nationality { Id = 123, Name = "Motswana" },
                            new Nationality { Id = 124, Name = "Mozambican" },
                            new Nationality { Id = 125, Name = "Namibian" },
                            new Nationality { Id = 126, Name = "Nauruan" },
                            new Nationality { Id = 127, Name = "Nepalese" },
                            new Nationality { Id = 128, Name = "New Zealander" },
                            new Nationality { Id = 129, Name = "Ni-Vanuatu" },
                            new Nationality { Id = 130, Name = "Nicaraguan" },
                            new Nationality { Id = 131, Name = "Nigerian" },
                            new Nationality { Id = 132, Name = "Nigerien" },
                            new Nationality { Id = 133, Name = "North Korean" },
                            new Nationality { Id = 134, Name = "Northern Irish" },
                            new Nationality { Id = 135, Name = "Norwegian" },
                            new Nationality { Id = 136, Name = "Omani" },
                            new Nationality { Id = 137, Name = "Pakistani" },
                            new Nationality { Id = 138, Name = "Palauan" },
                            new Nationality { Id = 139, Name = "Panamanian" },
                            new Nationality { Id = 140, Name = "Papua New Guinean" },
                            new Nationality { Id = 141, Name = "Paraguayan" },
                            new Nationality { Id = 142, Name = "Peruvian" },
                            new Nationality { Id = 143, Name = "Polish" },
                            new Nationality { Id = 144, Name = "Portuguese" },
                            new Nationality { Id = 145, Name = "Qatari" },
                            new Nationality { Id = 146, Name = "Romanian" },
                            new Nationality { Id = 147, Name = "Russian" },
                            new Nationality { Id = 148, Name = "Rwandan" },
                            new Nationality { Id = 149, Name = "Saint Lucian" },
                            new Nationality { Id = 150, Name = "Salvadoran" },
                            new Nationality { Id = 151, Name = "Samoan" },
                            new Nationality { Id = 152, Name = "San Marinese" },
                            new Nationality { Id = 153, Name = "Sao Tomean" },
                            new Nationality { Id = 154, Name = "Saudi" },
                            new Nationality { Id = 155, Name = "Scottish" },
                            new Nationality { Id = 156, Name = "Senegalese" },
                            new Nationality { Id = 157, Name = "Serbian" },
                            new Nationality { Id = 158, Name = "Seychellois" },
                            new Nationality { Id = 159, Name = "Sierra Leonean" },
                            new Nationality { Id = 160, Name = "Singaporean" },
                            new Nationality { Id = 161, Name = "Slovakian" },
                            new Nationality { Id = 162, Name = "Slovenian" },
                            new Nationality { Id = 163, Name = "Solomon Islander" },
                            new Nationality { Id = 164, Name = "Somali" },
                            new Nationality { Id = 165, Name = "South African" },
                            new Nationality { Id = 166, Name = "South Korean" },
                            new Nationality { Id = 167, Name = "Spanish" },
                            new Nationality { Id = 168, Name = "Sri Lankan" },
                            new Nationality { Id = 169, Name = "Sudanese" },
                            new Nationality { Id = 170, Name = "Surinamer" },
                            new Nationality { Id = 171, Name = "Swazi" },
                            new Nationality { Id = 172, Name = "Swedish" },
                            new Nationality { Id = 173, Name = "Swiss" },
                            new Nationality { Id = 174, Name = "Syrian" },
                            new Nationality { Id = 175, Name = "Taiwanese" },
                            new Nationality { Id = 176, Name = "Tajik" },
                            new Nationality { Id = 177, Name = "Tanzanian" },
                            new Nationality { Id = 178, Name = "Thai" },
                            new Nationality { Id = 179, Name = "Togolese" },
                            new Nationality { Id = 180, Name = "Tongan" },
                            new Nationality { Id = 181, Name = "Trinidadian/Tobagonian" },
                            new Nationality { Id = 182, Name = "Tunisian" },
                            new Nationality { Id = 183, Name = "Turkish" },
                            new Nationality { Id = 184, Name = "Tuvaluan" },
                            new Nationality { Id = 185, Name = "Ugandan" },
                            new Nationality { Id = 186, Name = "Ukrainian" },
                            new Nationality { Id = 187, Name = "Uruguayan" },
                            new Nationality { Id = 188, Name = "Uzbekistani" },
                            new Nationality { Id = 189, Name = "Venezuelan" },
                            new Nationality { Id = 190, Name = "Vietnamese" },
                            new Nationality { Id = 191, Name = "Welsh" },
                            new Nationality { Id = 192, Name = "Yemenite" },
                            new Nationality { Id = 193, Name = "Zambian" },
                            new Nationality { Id = 194, Name = "Zimbabwean" }
                        );
                    });

        }
    }
}



