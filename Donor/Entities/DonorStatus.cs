namespace Donor.Entities
{
    public enum DonorStatus
    {
        Applied  = 0 ,    // Default status
        Approved = 1,
        Rejected = 2,
        OnHold   = 3     // Requires a reason
    }

}
