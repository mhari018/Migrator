using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MigratorProduct.Dto
{
    public class Category
    {
        [JsonPropertyName("code")]
        public string code { get; set; }

        [JsonPropertyName("quantity_enabled")]
        public bool quantity_enabled { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; } = "";

        [JsonPropertyName("type")]
        public string type { get; set; } = "";

        [JsonPropertyName("lang")]
        public Lang lang { get; set; } = new Lang();

        [JsonPropertyName("constraints")]
        public Constraints constraints { get; set; } = new Constraints();

        [JsonPropertyName("default_code")]
        public string default_code { get; set; } = "";

        [JsonPropertyName("required")]
        public bool required { get; set; }

        [JsonPropertyName("sold_out_required")]
        public bool? sold_out_required { get; set; } = null;
    }
}
