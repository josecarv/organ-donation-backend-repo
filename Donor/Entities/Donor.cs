using Donor.Models;
using System.Text.Json.Serialization;


namespace Donor.Entities
{
    public class Donor
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string BloodType { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string ContactNumber { get; set; } = string.Empty;
        public string IdentityNumber { get; set; } = string.Empty;
        public string ResidentialAddress { get; set; } = string.Empty;
        public string MailingAddress { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TelephoneNumber { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        
        public PreferredContactMode PreferredContact { get; set; }
        public ICollection<Organ> Organs { get; set; } = new List<Organ>();


    }
     [JsonConverter(typeof(JsonStringEnumConverter))]
     public enum PreferredContactMode { SMS, EMAIL }

}
