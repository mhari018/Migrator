using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MigratorABC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "draft",
                table: "product",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_active_at",
                table: "product",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "merchandise_category_id",
                table: "product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "terms_and_condition",
                table: "product",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "draft",
                table: "product");

            migrationBuilder.DropColumn(
                name: "last_active_at",
                table: "product");

            migrationBuilder.DropColumn(
                name: "merchandise_category_id",
                table: "product");

            migrationBuilder.DropColumn(
                name: "terms_and_condition",
                table: "product");
        }
    }
}
