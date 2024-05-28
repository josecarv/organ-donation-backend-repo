namespace Donor.Entities
{
    public class DonationPreference
    {
        public int Id { get; set; }
        public int DonorId { get; set; }
        public Donor Donor { get; set; } = null!;
        public int OrganId { get; set; }
        public Organ Organ { get; set; } = null!;
    }
}
