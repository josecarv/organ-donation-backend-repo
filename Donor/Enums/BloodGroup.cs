using System.Text.Json.Serialization;

namespace Donor.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BloodGroup
    {
        A_Positive,
        A_Negative,
        B_Positive,
        B_Negative,
        AB_Positive,
        AB_Negative,
        O_Positive,
        O_Negative
    }
}
