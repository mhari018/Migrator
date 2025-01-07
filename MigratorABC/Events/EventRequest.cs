using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;

namespace MigratorABC.Events
{
    public class ProductAHMessage
    {
        [JsonPropertyName("article_code")]
        public string ArticleCode { get; set; }

        [JsonPropertyName("ah_node")]
        public string? AhNode { get; set; }

        [JsonPropertyName("valid_from")]
        public string? ValidFrom { get; set; }

        [JsonPropertyName("valid_to")]
        public string? ValidTo { get; set; }

        [JsonPropertyName("main")]
        public string? Main { get; set; }
    }

    public class ProductListingMessage
    {
        [JsonPropertyName("article")]
        public string Article { get; set; }

        [JsonPropertyName("site")]
        public string? Site { get; set; }

        [JsonPropertyName("valid_from")]
        public string? ValidFrom { get; set; }

        [JsonPropertyName("valid_to")]
        public string? ValidTo { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }


    public class MasterAHMessage
    {
        [JsonPropertyName("dept_id")]
        public string? DeptId { get; set; }

        [JsonPropertyName("dept_name")]
        public string? DeptName { get; set; }

        [JsonPropertyName("comm_id")]
        public string? CommId { get; set; }

        [JsonPropertyName("comm_name")]
        public string? CommName { get; set; }

        [JsonPropertyName("merch_id")]
        public string? MerchId { get; set; }

        [JsonPropertyName("merch_name")]
        public string? MerchName { get; set; }

        [JsonPropertyName("prodgrp_id")]
        public string? ProdgrpId { get; set; }

        [JsonPropertyName("prodgrp_name")]
        public string? ProdgrpName { get; set; }
    }

    public class ProductGeneralMessage
    {
        [JsonPropertyName("article_code")]
        public string ArticleCode { get; set; }

        [JsonPropertyName("article_desc")]
        public string? ArticleDesc { get; set; }

        [JsonPropertyName("brand_code")]
        public string? BrandCode { get; set; }

        [JsonPropertyName("brand_desc")]
        public string? BrandDesc { get; set; }

        [JsonPropertyName("base_uom")]
        public string? BaseUom { get; set; }

        [JsonPropertyName("article_category")]
        public string? ArticleCategory { get; set; }

        [JsonPropertyName("article_type")]
        public string? ArticleType { get; set; }

        [JsonPropertyName("old_article_code")]
        public string? OldArticleCode { get; set; }

        [JsonPropertyName("oldarticlenumber")]
        public string? OldArticleNumber { get; set; }

        [JsonPropertyName("length")]
        public string? Length { get; set; }

        [JsonPropertyName("width")]
        public string? Width { get; set; }

        [JsonPropertyName("height")]
        public string? Height { get; set; }

        [JsonPropertyName("dim_uom")]
        public string? DimUom { get; set; }

        [JsonPropertyName("gross_weight")]
        public string? GrossWeight { get; set; }

        [JsonPropertyName("net_weight")]
        public string? NetWeight { get; set; }

        [JsonPropertyName("weight_uom")]
        public string? WeightUom { get; set; }

        [JsonPropertyName("tax_class")]
        public string? TaxClass { get; set; }

        [JsonPropertyName("servc_aggr")]
        public string? ServcAggr { get; set; }

        [JsonPropertyName("servc_aggr_desc")]
        public string? ServcAggrDesc { get; set; }

        [JsonPropertyName("basic_text")]
        public string? BasicText { get; set; }

        [JsonPropertyName("tot_shelflife")]
        public string? TotShelflife { get; set; }

        [JsonPropertyName("rem_shelflife")]
        public string? RemShelflife { get; set; }
    }

    public class ProductPriceEventRequest
    {
        public string? LastModified { get; set; }

        [JsonPropertyName("SyncData")]
        public ProductPriceMessage? SyncData { get; set; }
    }

    public class ProductPriceMessage
    {
        [JsonPropertyName("priceA005")]
        public List<ProductPriceMessageBody>? PriceA005 { get; set; }

        [JsonPropertyName("priceA904")]
        public List<ProductPriceMessageBody>? PriceA904 { get; set; }

        [JsonPropertyName("priceA950")]
        public List<ProductPriceMessageBody>? PriceA950 { get; set; }

        [JsonPropertyName("priceA951")]
        public List<ProductPriceMessageBody>? PriceA951 { get; set; }

        [JsonPropertyName("priceA952")]
        public List<ProductPriceMessageBody>? PriceA952 { get; set; }

        [JsonPropertyName("priceA955")]
        public List<ProductPriceMessageBody>? PriceA955 { get; set; }

        [JsonPropertyName("priceA961")]
        public List<ProductPriceMessageBody>? PriceA961 { get; set; }

        [JsonPropertyName("priceA963")]
        public List<ProductPriceMessageBody>? PriceA963 { get; set; }

        [JsonPropertyName("priceA964")]
        public List<ProductPriceMessageBody>? PriceA964 { get; set; }

        [JsonPropertyName("priceA971")]
        public List<ProductPriceMessageBody>? PriceA971 { get; set; }

        [JsonPropertyName("priceA973")]
        public List<ProductPriceMessageBody>? PriceA973 { get; set; }
    }

    public class ProductPriceMessageBody
    {
        [JsonPropertyName("client")]
        public int? Client { get; set; }

        [JsonPropertyName("application")]
        public string? Application { get; set; }

        [JsonPropertyName("cond_type")]
        public string? CondType { get; set; }

        [JsonPropertyName("sales_org")]
        public string? SalesOrg { get; set; }

        [JsonPropertyName("dist_channel")]
        public int DistChannel { get; set; }

        [JsonPropertyName("price_list")]
        public string? PriceList { get; set; }

        [JsonPropertyName("site")]
        public string? Site { get; set; }

        [JsonPropertyName("sales_office")]
        public string? SalesOffice { get; set; }

        [JsonPropertyName("brand")]
        public string? Brand { get; set; }

        [JsonPropertyName("customer")]
        public string? Customer { get; set; }

        [JsonPropertyName("article")]
        public string Article { get; set; }

        [JsonPropertyName("valid_from")]
        public string ValidFrom { get; set; }

        [JsonPropertyName("valid_to")]
        public string ValidTo { get; set; }

        [JsonPropertyName("cond_rec_no")]
        public string CondRecNo { get; set; }

        [JsonPropertyName("value")]
        public long Value { get; set; }

        [JsonPropertyName("created_on")]
        public string CreatedOn { get; set; }

        [JsonPropertyName("del_flag")]
        public string? DelFlag { get; set; }

    }
}
