using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addSelectedTourGuideTableInDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SelectedTourGuides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourGuideId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelectedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SelectedTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    Adults = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedTourGuides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectedTourGuides_AspNetUsers_TourGuideId",
                        column: x => x.TourGuideId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelectedTourGuides_TourGuideId",
                table: "SelectedTourGuides",
                column: "TourGuideId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectedTourGuides");
        }
    }
}
