using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MigratorProduct.Dto
{
    public class Combination
    {
        [JsonPropertyName("combination_code")]
        public List<string> combination_code { get; set; } = new List<string>();

        [JsonPropertyName("result_code")]
        public string result_code { get; set; }

        [JsonPropertyName("characteristic")]
        public List<Characteristic> characteristic { get; set; } = new List<Characteristic>();

        [JsonPropertyName("required_option_codes")]
        public List<string> required_option_codes { get; set; } = new List<string>();
    }
}
