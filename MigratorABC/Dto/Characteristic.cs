using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MigratorProduct.Dto
{
    public class Characteristic
    {
        [JsonPropertyName("name")]
        public string name { get; set; }
    }
}
