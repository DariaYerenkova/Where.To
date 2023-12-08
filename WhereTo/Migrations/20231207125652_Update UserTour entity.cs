using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhereTo.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserTourentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateRegistered",
                table: "UserTours",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsPayed",
                table: "UserTours",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateRegistered",
                table: "UserTours");

            migrationBuilder.DropColumn(
                name: "IsPayed",
                table: "UserTours");
        }
    }
}
