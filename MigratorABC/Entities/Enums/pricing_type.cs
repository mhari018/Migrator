using NpgsqlTypes;

namespace MigratorProduct.Entities.Enums
{
    [PgName("pricing_type")]
    public enum pricing_type
    {
        [PgName("apps")]
        apps,

        [PgName("grab")]
        grab
    }
}
