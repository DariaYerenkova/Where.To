using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhereToDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class upduserhotels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserHotels_Users_UserId",
                table: "UserHotels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserHotels",
                table: "UserHotels");

            migrationBuilder.RenameTable(
                name: "UserHotels",
                newName: "UserHotels");

            migrationBuilder.RenameIndex(
                name: "IX_UserHotels_UserId",
                table: "UserHotels",
                newName: "IX_UserHotels_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserHotels",
                table: "UserHotels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserHotels_Users_UserId",
                table: "UserHotels",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserHotels_Users_UserId",
                table: "UserHotels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserHotels",
                table: "UserHotels");

            migrationBuilder.RenameTable(
                name: "UserHotels",
                newName: "UserHotels");

            migrationBuilder.RenameIndex(
                name: "IX_UserHotels_UserId",
                table: "UserHotels",
                newName: "IX_UserHotels_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserHotels",
                table: "UserHotels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserHotels_Users_UserId",
                table: "UserHotels",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
