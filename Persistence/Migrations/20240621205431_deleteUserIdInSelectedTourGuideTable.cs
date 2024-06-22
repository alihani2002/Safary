using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class deleteUserIdInSelectedTourGuideTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SelectedTourGuides_TouristId_TourguideId_SelectedDate",
                table: "SelectedTourGuides");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SelectedTourGuides");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "SelectedTime",
                table: "SelectedTourGuides",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SelectedDate",
                table: "SelectedTourGuides",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SelectedTourGuides_TouristId_TourguideId_SelectedDate",
                table: "SelectedTourGuides",
                columns: new[] { "TouristId", "TourguideId", "SelectedDate" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SelectedTourGuides_TouristId_TourguideId_SelectedDate",
                table: "SelectedTourGuides");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "SelectedTime",
                table: "SelectedTourGuides",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SelectedDate",
                table: "SelectedTourGuides",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SelectedTourGuides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedTourGuides_TouristId_TourguideId_SelectedDate",
                table: "SelectedTourGuides",
                columns: new[] { "TouristId", "TourguideId", "SelectedDate" },
                unique: true,
                filter: "[SelectedDate] IS NOT NULL");
        }
    }
}
