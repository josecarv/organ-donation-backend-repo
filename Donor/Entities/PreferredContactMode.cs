
using System.Text.Json.Serialization;

namespace Donor.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PreferredContactMode
    {
        SMS = 0 ,
        EMAIL = 1
    }
}
