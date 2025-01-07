using NpgsqlTypes;

namespace MigratorProduct.Entities.Enums
{
    [PgName("product_type")]
    public enum ProductType
    {
        [PgName("single")]
        Single,

        [PgName("group")]
        Group
    }
}
