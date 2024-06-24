using Donor.Models;
using System.Text.Json.Serialization;


namespace Donor.Entities
{
    public class Donor
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string BloodGroup { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string IdentityNumber { get; set; } = string.Empty;
        public Address ResidentialAddress { get; set; } = new Address();
        public Address MailingAddress { get; set; } = new Address();
        public string Email { get; set; } = string.Empty;
        public string TelephoneNumber { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;

        public int PreferredContact { get; set; }

        public int DonorStatus { get; set; }
        public string OnHoldReason { get; set; }

       // public string ModifiedBy { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Organ> Organs { get; set; } = new List<Organ>();


    }
  

}
