using System.Text.Json.Serialization;

namespace MigratorProduct.Dto
{
    public class Platform
    {
        [JsonPropertyName("GRAB")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PlatformData? GRAB { get; set; }

        [JsonPropertyName("POS")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PlatformData? POS { get; set; }

        [JsonPropertyName("APPS")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PlatformData? APPS { get; set; }
    }
}
