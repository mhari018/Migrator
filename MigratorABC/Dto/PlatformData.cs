using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MigratorProduct.Dto
{
    public class PlatformData
    {
        [JsonPropertyName("options")]
        public List<Option> Options { get; set; } = new List<Option>();

        [JsonPropertyName("categories")]
        public List<Category> Categories { get; set; } = new List<Category>();

        [JsonPropertyName("combination")]
        public List<Combination> Combination { get; set; } = new List<Combination>();

        [JsonPropertyName("metadata")]
        public PlatformMetadata Metadata { get; set; } = new PlatformMetadata();
    }

    public class PlatformMetadata
    {
        [JsonPropertyName("selling")]
        public bool Selling { get; set; }

        [JsonPropertyName("external")]
        public bool External { get; set; }

        [JsonPropertyName("pickup")]
        public bool Pickup { get; set; }

        [JsonPropertyName("delivery")]
        public bool Delivery { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
    }
}
