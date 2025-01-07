using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigratorProduct.Entities
{
    [Table("option_group")]
    public class OptionGroup : BaseEntity
    {
        [MaxLength(150)]
        [Column("name")]
        public string Name { get; set; }

        [MaxLength(50)]
        [Column("type")]
        public string? Type { get; set; }

        [MaxLength(25)]
        [Column("SI001")]
        public string? SI001 { get; set; }

        [MaxLength(25)]
        [Column("SI002")]
        public string? SI002 { get; set; }

        [MaxLength(25)]
        [Column("SI003")]
        public string? SI003 { get; set; }

        [MaxLength(25)]
        [Column("SI007")]
        public string? SI007 { get; set; }
    }
}
