using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MigratorProduct.Dto
{
    public class Product
    {
        [JsonPropertyName("id")]
        public string id { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; }

        private string _displayName;

        [JsonPropertyName("display_name")]
        public string display_name
        {
            get => string.IsNullOrEmpty(_displayName) ? name : _displayName;
            set => _displayName = value;
        }

        [JsonPropertyName("brand")]
        public string brand { get; set; }

        [JsonPropertyName("description")]
        public string description { get; set; }

        [JsonPropertyName("image_url")]
        public string image_url { get; set; }

        [JsonPropertyName("merchandise_category_id")]
        public string merchandise_category_id { get; set; }

        [JsonPropertyName("terms_and_condition")]
        public string terms_and_condition { get; set; }

        [JsonPropertyName("draft")]
        public bool Draft { get; set; } = false;

        [JsonPropertyName("metadata")]
        public Metadata metadata { get; set; } = new Metadata();

        [JsonPropertyName("categories")]
        public List<Category> categories { get; set; } = new List<Category>();

        [JsonPropertyName("site_listing")]
        public List<string> site_listing { get; set; } = new List<string>();

        [JsonPropertyName("available_platform")]
        public List<string> available_platform { get; set; } = new List<string>();

        [JsonPropertyName("combination")]
        public List<Combination> combination { get; set; } = new List<Combination>();
        [JsonPropertyName("characteristic")]
        public List<Characteristic> characteristic { get; set; } = new List<Characteristic>();

        [JsonPropertyName("platform")]
        public Platform platform { get; set; } = new Platform();

        [JsonPropertyName("options")]
        public List<Option> options { get; set; } = new List<Option>();

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? updated_at { get; set; }

        [JsonPropertyName("updated_by")]
        public string updated_by { get; set; }

        [JsonPropertyName("active_at")]
        public DateTime? active_at { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public string _Id { get; set; }

        [JsonPropertyName("last_active_at")]
        public string? last_active_at { get; set; }
    }
}
