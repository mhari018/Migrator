using MigratorProduct.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace MigratorProduct.Entities
{
    [Table("product")]
    public class Product : BaseEntity
    {
        [MaxLength(150)]
        [Column("name")]
        public string Name { get; set; }

        [MaxLength(150)]
        [Column("display_name")]
        public string DisplayName { get; set; }

        [MaxLength(500)]
        [Column("description")]
        public string? Description { get; set; }


        [Column("image_url")]
        public string ImageUrl { get; set; }

        [MaxLength(150)]
        [Column("metadata_name")]
        public string MetadataName { get; set; }

        [MaxLength(36)]
        [Column("metadata_code")]
        public string MetadataCode { get; set; }

        [Column("metadata_pickup")]
        public bool MetadataPickup { get; set; } = false;

        [Column("metadata_selling")]
        public bool MetadataSelling { get; set; } = false;

        [Column("metadata_delivery")]
        public bool MetadataDelivery { get; set; } = false;

        [Column("metadata_external")]
        public bool MetadataExternal { get; set; } = false;

        [Column("metadata_category")]
        public JsonDocument? MetadataCategory { get; set; }

        [Column("metadata_series")]
        public JsonDocument? MetadataSeries { get; set; }

        [Column("merchandise_category_id")]
        public string? MerchandiseCategoryId { get; set; }

        [Column("terms_and_condition")]
        public string? TermsAndCondition { get; set; }

        [Column("draft")]
        public bool Draft { get; set; } = false;   

        [Column("site_listing")]
        public List<string>? SiteListing { get; set; } = new List<string>();

        [MaxLength(4)]
        [Column("characteristic")]
        public JsonDocument? Characteristic { get; set; }

        [MaxLength(25)]
        [Column("available_platform")]
        public List<string>? AvailablePlatform { get; set; } = new List<string>();

        [Column("active_at")]
        public DateTime? ActiveAt { get; set; }

        [MaxLength(50)]
        [Column("brand")]
        public string Brand { get; set; }

        [Column("type")]
        public ProductType Type { get; set; }

        [Column("last_active_at")]
        public DateTime? LastActiveAt { get; set; }

        [Column("platform")]
        public JsonDocument? Platform { get; set; }

        public Collection<ProductCategory> Categories { get; set; }
        public Collection<ProductOption> Options { get; set; }
        public Collection<ProductCombination> Combinations { get; set; }
    }
}
