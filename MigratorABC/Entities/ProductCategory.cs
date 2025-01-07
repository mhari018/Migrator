using MigratorProduct.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MigratorProduct.Entities
{
    [Table("product_category")]
    public class ProductCategory : BaseEntity
    {
        [Column("product_id")]
        public Guid ProductId { get; set; }

        [MaxLength(150)]
        [Column("name")]
        public string Name { get; set; }

        [MaxLength(36)]
        [Column("code")]
        public string Code { get; set; }

        [Column("sold_out_required")]
        public bool SoldOutRequired { get; set; } = false;

        [Column("quantity_enabled")]
        public bool QuantityEnabled { get; set; } = false;

        [MaxLength(36)]
        [Column("default_code")]
        public string DefaultCode { get; set; }

        [Column("required")]
        public bool Required { get; set; } = false;

        [Column("type")]
        public ProductCategoryType Type { get; set; }

        [Column("lang")]
        public JsonDocument? Lang { get; set; }

        [Column("constrains")]
        public JsonDocument? Constrains { get; set; }
    }
}
