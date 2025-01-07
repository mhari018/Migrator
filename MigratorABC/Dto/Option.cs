using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MigratorProduct.Dto
{
    public class Option
    {
        [JsonPropertyName("product")]
        public CodeName Product { get; set; } = new CodeName();

        [JsonPropertyName("available_on")]
        public List<string> available_on { get; set; } = new List<string>();

        [JsonPropertyName("code")]
        public string code { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("description")]
        public string description { get; set; }

        [JsonPropertyName("category")]
        public string category { get; set; }
    }
}
