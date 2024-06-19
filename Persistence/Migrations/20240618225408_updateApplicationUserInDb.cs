using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateApplicationUserInDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "AspNetUsers",
                newName: "SelectedDate");

            migrationBuilder.AddColumn<int>(
                name: "Adults",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasCar",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ReviewsNumber",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SelectedTime",
                table: "AspNetUsers",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adults",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HasCar",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReviewsNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SelectedTime",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "SelectedDate",
                table: "AspNetUsers",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }
    }
}
