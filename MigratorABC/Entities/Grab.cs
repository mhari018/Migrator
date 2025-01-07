using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MigratorABC.Entities
{
    public class Grab
    {
        [JsonPropertyName("price_without_tax")]
        public int? PriceWithoutTax { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("price")]
        public int Price { get; set; }

        [JsonPropertyName("sold_out")]
        public bool SoldOut { get; set; }

        [JsonPropertyName("updated_by")]
        public string UpdatedBy { get; set; }

        [JsonPropertyName("tax")]
        public int? Tax { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; } = true;
    }
}
