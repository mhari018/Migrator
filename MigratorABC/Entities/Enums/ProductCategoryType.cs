using NpgsqlTypes;

namespace MigratorProduct.Entities.Enums
{
    [PgName("product_category_type")]
    public enum ProductCategoryType
    {
        [PgName("radio")]
        Radio,

        [PgName("checkbox")]
        Checkbox,

        [PgName("auto_apply")]
        AutoApply
    }
}
