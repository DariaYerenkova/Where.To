using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhereTo.Migrations
{
    /// <inheritdoc />
    public partial class addcommentstables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TourFeedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourFeedbacks_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourFeedbacks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlobAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourFeedbackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlobAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlobAttachments_TourFeedbacks_TourFeedbackId",
                        column: x => x.TourFeedbackId,
                        principalTable: "TourFeedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlobAttachments_TourFeedbackId",
                table: "BlobAttachments",
                column: "TourFeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_TourFeedbacks_TourId",
                table: "TourFeedbacks",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourFeedbacks_UserId",
                table: "TourFeedbacks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlobAttachments");

            migrationBuilder.DropTable(
                name: "TourFeedbacks");
        }
    }
}
