using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addSelectedTourGuideTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adults",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SelectedDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SelectedTime",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "Reviews",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SelectedTourGuides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TouristId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TourguideId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelectedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SelectedTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    Adults = table.Column<int>(type: "int", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ApplicationUserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedTourGuides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectedTourGuides_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SelectedTourGuides_AspNetUsers_ApplicationUserId1",
                        column: x => x.ApplicationUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SelectedTourGuides_AspNetUsers_TourguideId",
                        column: x => x.TourguideId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SelectedTourGuides_AspNetUsers_TouristId",
                        column: x => x.TouristId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelectedTourGuides_ApplicationUserId",
                table: "SelectedTourGuides",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedTourGuides_ApplicationUserId1",
                table: "SelectedTourGuides",
                column: "ApplicationUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedTourGuides_TourguideId",
                table: "SelectedTourGuides",
                column: "TourguideId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedTourGuides_TouristId_TourguideId_SelectedDate",
                table: "SelectedTourGuides",
                columns: new[] { "TouristId", "TourguideId", "SelectedDate" },
                unique: true,
                filter: "[SelectedDate] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectedTourGuides");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "LastUpdatedOn",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "Adults",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SelectedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SelectedTime",
                table: "AspNetUsers",
                type: "time",
                nullable: true);
        }
    }
}
