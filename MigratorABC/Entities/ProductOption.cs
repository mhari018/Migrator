using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MigratorProduct.Entities
{
    [Table("product_option")]
    public class ProductOption : BaseEntity
    {
        [Column("product_id")]
        public Guid ProductId { get; set; }

        [MaxLength(150)]
        [Column("name")]
        public string Name { get; set; }

        [MaxLength(36)]
        [Column("code")]
        public string Code { get; set; }

        [Column("product")]
        public JsonDocument? Product { get; set; }

        [Column("available_on")]
        public List<string> AvailableOn { get; set; } = new List<string>();

        [MaxLength(500)]
        [Column("description")]
        public string Description { get; set; }

        [MaxLength(50)]
        [Column("category")]
        public string Category { get; set; }
    }
}
