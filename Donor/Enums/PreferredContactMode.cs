
using System.Text.Json.Serialization;

namespace Donor.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PreferredContactMode
    {
        SMS = 0,
        EMAIL = 1
    }
}
