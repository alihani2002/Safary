using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TourDays_TourDayId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_tourHours_Cities_CityId",
                table: "tourHours");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "TourDays");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_tourHours_CityId",
                table: "tourHours");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TourDayId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "tourHours");

            migrationBuilder.DropColumn(
                name: "TourDayId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "tourHours",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TourDayId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TourDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourDays_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TourDayId = table.Column<int>(type: "int", nullable: true),
                    TourHourId = table.Column<int>(type: "int", nullable: true),
                    TourId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Places_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Places_TourDays_TourDayId",
                        column: x => x.TourDayId,
                        principalTable: "TourDays",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Places_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Places_tourHours_TourHourId",
                        column: x => x.TourHourId,
                        principalTable: "tourHours",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tourHours_CityId",
                table: "tourHours",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TourDayId",
                table: "AspNetUsers",
                column: "TourDayId",
                unique: true,
                filter: "[TourDayId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_CityId",
                table: "Places",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_TourDayId",
                table: "Places",
                column: "TourDayId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_TourHourId",
                table: "Places",
                column: "TourHourId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_TourId",
                table: "Places",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourDays_CityId",
                table: "TourDays",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TourDays_TourDayId",
                table: "AspNetUsers",
                column: "TourDayId",
                principalTable: "TourDays",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tourHours_Cities_CityId",
                table: "tourHours",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }
    }
}
