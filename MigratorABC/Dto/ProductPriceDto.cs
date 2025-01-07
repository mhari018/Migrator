using MigratorABC.Entities;
using MigratorProduct.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MigratorABC.Dto
{
    public class ProductPriceDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("product_code")]
        public string product_code { get; set; }

        [JsonPropertyName("site_code")]
        public string site_code { get; set; }

        [JsonPropertyName("price")]
        public int Price { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; } = new DateTime();

        [JsonPropertyName("created_by")]
        public string created_by { get; set; } = "importer";

        [JsonPropertyName("updated_at")]
        public DateTime? updated_at { get; set; } = new DateTime();

        [JsonPropertyName("updated_by")]
        public string updated_by { get; set; } = "importer";

        [JsonPropertyName("pim_updated_at")]
        public DateTime? PimUpdatedAt { get; set; }

        [JsonPropertyName("price_without_tax")]
        public string PriceWithoutTax { get; set; } = "0";

        [JsonPropertyName("sold_out")]
        public bool SoldOut { get; set; }

        [JsonPropertyName("tax")]
        public string Tax { get; set; } = "0";

        [JsonPropertyName("grab")]
        public Grab? Grab { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; } = true;

        [JsonIgnore]
        public string IndexID { get; set; }
    }
}
