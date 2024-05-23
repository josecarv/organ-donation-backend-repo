using System;
using System.ComponentModel.DataAnnotations;

namespace Donor.Models
{
    public class DonorDto
    {
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Full name can't be longer than 30 characters")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Blood type is required")]
        [StringLength(3, ErrorMessage = "Blood type can't be longer than 3 characters")]
        public string BloodType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [StringLength(15, ErrorMessage = "Contact number can't be longer than 15 characters")]
        public string ContactNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Identity number is required")]
        [StringLength(50, ErrorMessage = "Identity number can't be longer than 50 characters")]
        public string IdentityNumber { get; set; } = string.Empty;

        [StringLength(250, ErrorMessage = "Residential address can't be longer than 250 characters")]
        public string ResidentialAddress { get; set; } = string.Empty;

        [StringLength(250, ErrorMessage = "Mailing address can't be longer than 250 characters")]
        public string MailingAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [StringLength(15, ErrorMessage = "Telephone number can't be longer than 15 characters")]
        public string TelephoneNumber { get; set; } = string.Empty;

        [StringLength(15, ErrorMessage = "Mobile number can't be longer than 15 characters")]
        public string MobileNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nationality is required")]
        public string  Nationality { get; set; }  = string.Empty;

        [Required(ErrorMessage = "Gender is required")]
        public string  Gender { get; set; }  = string.Empty;

    }
}
