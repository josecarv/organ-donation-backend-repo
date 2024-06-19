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
        
        [Required(ErrorMessage = "First name is required")]
        [StringLength(100, ErrorMessage = "First name can't be longer than 100 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(100, ErrorMessage = "Last name can't be longer than 100 characters")]
        public string LastName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Blood group is required")]
        [StringLength(10, ErrorMessage = "Blood type can't be longer than 10 characters")]
        public string BloodGroup { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Identity number is required")]
        [StringLength(50, ErrorMessage = "Identity number can't be longer than 50 characters")]
        public string IdentityNumber { get; set; } = string.Empty;

        public Address ResidentialAddress { get; set; } = new Address();
        public Address MailingAddress { get; set; } = new Address();

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100, ErrorMessage = "Email can't be longer than 100 characters")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telephone number is required")]
        [StringLength(15, ErrorMessage = "Telephone number can't be longer than 15 characters")]
        public string TelephoneNumber { get; set; } = string.Empty;

        [StringLength(15, ErrorMessage = "Mobile number can't be longer than 15 characters")]
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
