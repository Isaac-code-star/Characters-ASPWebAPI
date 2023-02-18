using System.Text.Json.Serialization;

namespace ASPWebAPI.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum rpgClass
    {
        Knight = 1,
        Mage = 2, 
        Cleric = 3,
    }
}
