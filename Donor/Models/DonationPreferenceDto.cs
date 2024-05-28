namespace Donor.Models
{
    public class DonationPreferenceDto
    {
        public int Id { get; set; }
        public int DonorId { get; set; }
        public int OrganId { get; set; }
        public string OrganName { get; set; }  = string.Empty;

    }
}
