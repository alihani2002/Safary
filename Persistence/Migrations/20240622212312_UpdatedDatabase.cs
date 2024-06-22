using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TourHourId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "tourHours");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "tourHours");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "tourHours");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "tourHours");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "tourHours");

            migrationBuilder.AddColumn<int>(
                name: "TourId",
                table: "SelectedTourGuides",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TourName",
                table: "SelectedTourGuides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedTourGuides_TourId",
                table: "SelectedTourGuides",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TourHourId",
                table: "AspNetUsers",
                column: "TourHourId");

            migrationBuilder.AddForeignKey(
                name: "FK_SelectedTourGuides_tourHours_TourId",
                table: "SelectedTourGuides",
                column: "TourId",
                principalTable: "tourHours",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelectedTourGuides_tourHours_TourId",
                table: "SelectedTourGuides");

            migrationBuilder.DropIndex(
                name: "IX_SelectedTourGuides_TourId",
                table: "SelectedTourGuides");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TourHourId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TourId",
                table: "SelectedTourGuides");

            migrationBuilder.DropColumn(
                name: "TourName",
                table: "SelectedTourGuides");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "tourHours",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "tourHours",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "tourHours",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "tourHours",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "tourHours",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TourHourId",
                table: "AspNetUsers",
                column: "TourHourId",
                unique: true,
                filter: "[TourHourId] IS NOT NULL");
        }
    }
}
