using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MigratorProduct.Entities
{
    [Table("product_combination")]
    public class ProductCombination : BaseEntity
    {
        [Column("product_id")]
        public Guid ProductId { get; set; }

        [Column("required_option_codes")]
        public List<string> RequiredOptionCodes { get; set; } = new List<string>();

        [MaxLength(36)]
        [Column("combination_code")]
        public List<string> CombinationCode { get; set; } = new List<string>();

        [MaxLength(36)]
        [Column("result_code")]
        public string ResultCode { get; set; }

        [MaxLength(4)]
        [Column("characteristic")]
        public JsonDocument? Characteristic { get; set; }
    }
}
