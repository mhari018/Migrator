using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MigratorProduct.Dto
{
    public class Lang
    {
        [JsonPropertyName("en")]
        public string en { get; set; }

        [JsonPropertyName("id")]
        public string id { get; set; }
    }
}
