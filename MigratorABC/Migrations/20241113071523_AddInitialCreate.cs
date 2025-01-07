using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using MigratorProduct.Entities.Enums;

#nullable disable

namespace MigratorABC.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:product_category_type", "radio,checkbox,auto_apply")
                .Annotation("Npgsql:Enum:product_type", "single,group");

            migrationBuilder.CreateTable(
                name: "option_group",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    SI001 = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SI002 = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SI003 = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SI007 = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_option_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    display_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    image_url = table.Column<string>(type: "text", nullable: false),
                    metadata_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    metadata_code = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    metadata_pickup = table.Column<bool>(type: "boolean", nullable: false),
                    metadata_selling = table.Column<bool>(type: "boolean", nullable: false),
                    metadata_delivery = table.Column<bool>(type: "boolean", nullable: false),
                    metadata_external = table.Column<bool>(type: "boolean", nullable: false),
                    metadata_category = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    metadata_series = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    site_listing = table.Column<List<string>>(type: "text[]", maxLength: 4, nullable: true),
                    characteristic = table.Column<List<string>>(type: "text[]", maxLength: 4, nullable: true),
                    available_platform = table.Column<List<string>>(type: "text[]", maxLength: 25, nullable: true),
                    active_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    brand = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    type = table.Column<ProductType>(type: "product_type", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_price",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    site_code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false),
                    price_without_tax = table.Column<int>(type: "integer", nullable: false),
                    tax = table.Column<int>(type: "integer", nullable: false),
                    sold_out = table.Column<bool>(type: "boolean", nullable: false),
                    pricing_type = table.Column<int>(type: "integer", nullable: false),
                    pim_updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_price", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    code = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    sold_out_required = table.Column<bool>(type: "boolean", nullable: false),
                    quantity_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    default_code = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    required = table.Column<bool>(type: "boolean", nullable: false),
                    type = table.Column<ProductCategoryType>(type: "product_category_type", nullable: false),
                    lang = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    constrains = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_category", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_category_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_combination",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    required_option_codes = table.Column<List<string>>(type: "text[]", nullable: false),
                    combination_code = table.Column<List<string>>(type: "text[]", maxLength: 36, nullable: false),
                    result_code = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    characteristic = table.Column<List<string>>(type: "text[]", maxLength: 4, nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_combination", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_combination_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_option",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    code = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    product = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    available_on = table.Column<List<string>>(type: "text[]", nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_option", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_option_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_category_product_id",
                table: "product_category",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_combination_product_id",
                table: "product_combination",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_option_product_id",
                table: "product_option",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "option_group");

            migrationBuilder.DropTable(
                name: "product_category");

            migrationBuilder.DropTable(
                name: "product_combination");

            migrationBuilder.DropTable(
                name: "product_option");

            migrationBuilder.DropTable(
                name: "product_price");

            migrationBuilder.DropTable(
                name: "product");
        }
    }
}
