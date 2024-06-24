using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForiegnKeyTourName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourReviews_Tours_TourId",
                table: "TourReviews");

            migrationBuilder.RenameColumn(
                name: "TourId",
                table: "TourReviews",
                newName: "TourName");

            migrationBuilder.RenameIndex(
                name: "IX_TourReviews_TourId",
                table: "TourReviews",
                newName: "IX_TourReviews_TourName");

            migrationBuilder.AddForeignKey(
                name: "FK_TourReviews_Tours_TourName",
                table: "TourReviews",
                column: "TourName",
                principalTable: "Tours",
                principalColumn: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourReviews_Tours_TourName",
                table: "TourReviews");

            migrationBuilder.RenameColumn(
                name: "TourName",
                table: "TourReviews",
                newName: "TourId");

            migrationBuilder.RenameIndex(
                name: "IX_TourReviews_TourName",
                table: "TourReviews",
                newName: "IX_TourReviews_TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourReviews_Tours_TourId",
                table: "TourReviews",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Name");
        }
    }
}
