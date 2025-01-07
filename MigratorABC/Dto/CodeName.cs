using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MigratorProduct.Dto
{
    public class CodeName
    {
        public CodeName()
        {

        }

        public CodeName(string code, string name, string? selling_name = null)
        {
            this.code = code;
            this.name = name;
        }

        [JsonPropertyName("code")]
        public string code { get; set; } = "";

        [JsonPropertyName("name")]
        public string name { get; set; } = "";
    }
}
