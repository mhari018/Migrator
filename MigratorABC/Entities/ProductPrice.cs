using MigratorABC.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace MigratorProduct.Entities
{
    [Table("product_price")]
    public class ProductPrice : BaseEntity
    {
        [MaxLength(36)]
        [Column("product_code")]
        public string ProductCode { get; set; }

        [MaxLength(4)]
        [Column("site_code")]
        public string SiteCode { get; set; }

        [Column("price")]
        public int Price { get; set; }

        [Column("price_without_tax")]
        public int PriceWithoutTax { get; set; } 
        
        [Column("tax")]
        public int Tax { get; set; }

        [Column("sold_out")]
        public bool SoldOut { get; set; }

        //[Column("pricing_type")]
        //public pricing_type PricingType { get; set; }

        [Column("sap_updated_at")]
        public DateTime? SapUpdatedAt { get; set; }

        [Column("active")]
        public bool? Active { get; set; } = default!;

        [Column("dist_channel")]
        public short DistChannel { get; set; }
    }
}
