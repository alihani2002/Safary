using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTourImagesTableAndUpdateNameInSelectedTourGuideTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelectedTourGuides_Tours_TourName",
                table: "SelectedTourGuides");

            migrationBuilder.AlterColumn<string>(
                name: "TourName",
                table: "SelectedTourGuides",
                type: "nvarchar(150)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)");

            

            migrationBuilder.AddForeignKey(
                name: "FK_SelectedTourGuides_Tours_TourName",
                table: "SelectedTourGuides",
                column: "TourName",
                principalTable: "Tours",
                principalColumn: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelectedTourGuides_Tours_TourName",
                table: "SelectedTourGuides");

            migrationBuilder.AlterColumn<string>(
                name: "TourName",
                table: "SelectedTourGuides",
                type: "nvarchar(150)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SelectedTourGuides_Tours_TourName",
                table: "SelectedTourGuides",
                column: "TourName",
                principalTable: "Tours",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
