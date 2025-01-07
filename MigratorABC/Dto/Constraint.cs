using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MigratorProduct.Dto
{
    public class Constraints
    {
        [JsonPropertyName("required_items")]
        public string required_items { get; set; } = "";

        /// <summary>
        /// for Phoenix
        /// </summary>
        [JsonPropertyName("required_item")]
        public string required_item
        {
            get => required_items;
            set => required_items = value;
        }

        [JsonPropertyName("maximum_choices")]
        public int maximum_choices { get; set; }

        [JsonPropertyName("minimum_choices")]
        public int minimum_choices { get; set; }
    }
}
