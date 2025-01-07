using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MigratorProduct.Dto
{
    public class OptionGroup
    {
        [JsonPropertyName("id")]
        public Guid ID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string? Type {  get; set; }

        [JsonPropertyName("SI001")]
        public string SI001 { get; set; }

        [JsonPropertyName("SI002")]
        public string SI002 { get; set; }

        [JsonPropertyName("SI003")]
        public string SI003 { get; set; }

        [JsonPropertyName("SI007")]
        public string SI007 { get; set; }

        [JsonPropertyName("updated_by")]
        public string? UpdatedBy { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; } = default!;

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
