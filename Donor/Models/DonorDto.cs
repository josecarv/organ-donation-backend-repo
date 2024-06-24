using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Donor.Entities;

namespace Donor.Models
{
    public class DonorDto
    {
        public int? Id { get; set; }
    
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

        public PreferredContactMode PreferredContact { get; set; }

        public DonorStatus DonorStatus { get; set; }

        public string OnHoldReason { get; set; } = string.Empty;

       /// <summary>
       /// public string ModifiedBy { get; set; }
       /// </summary>

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<int> Organs { get; set; } = new List<int>();


        
    }

}
