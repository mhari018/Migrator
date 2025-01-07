using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MigratorProduct.Dto
{
    public class Metadata
    {
        public Metadata()
        {

        }

        public Metadata(string code, bool delivery, bool external, CodeName series, string name, bool pickup, bool selling, CodeName category)
        {
            Code = code;
            Delivery = delivery;
            External = external;
            Series = series;
            Name = name;
            Pickup = pickup;
            Selling = selling;
            Category = category;
        }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("delivery")]
        public bool Delivery { get; set; }

        [JsonPropertyName("external")]
        public bool External { get; set; }

        [JsonPropertyName("series")]
        public CodeName Series { get; set; } = new CodeName();

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("pickup")]
        public bool Pickup { get; set; }

        [JsonPropertyName("selling")]
        public bool Selling { get; set; }

        [JsonPropertyName("category")]
        public CodeName Category { get; set; } = new CodeName();
    }
}
