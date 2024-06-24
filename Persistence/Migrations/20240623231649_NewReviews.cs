using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourGuideReviews_AspNetUsers_TourGuideId",
                table: "TourGuideReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_TourGuideReviews_AspNetUsers_TouristId",
                table: "TourGuideReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_TourReviews_AspNetUsers_TouristId",
                table: "TourReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_TourReviews_Tours_TourId",
                table: "TourReviews");

            migrationBuilder.AlterColumn<string>(
                name: "TouristId",
                table: "TourReviews",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "TourId",
                table: "TourReviews",
                type: "nvarchar(150)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)");

            migrationBuilder.AlterColumn<string>(
                name: "TouristId",
                table: "TourGuideReviews",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "TourGuideId",
                table: "TourGuideReviews",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_TourGuideReviews_AspNetUsers_TourGuideId",
                table: "TourGuideReviews",
                column: "TourGuideId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourGuideReviews_AspNetUsers_TouristId",
                table: "TourGuideReviews",
                column: "TouristId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourReviews_AspNetUsers_TouristId",
                table: "TourReviews",
                column: "TouristId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourReviews_Tours_TourId",
                table: "TourReviews",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourGuideReviews_AspNetUsers_TourGuideId",
                table: "TourGuideReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_TourGuideReviews_AspNetUsers_TouristId",
                table: "TourGuideReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_TourReviews_AspNetUsers_TouristId",
                table: "TourReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_TourReviews_Tours_TourId",
                table: "TourReviews");

            migrationBuilder.AlterColumn<string>(
                name: "TouristId",
                table: "TourReviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TourId",
                table: "TourReviews",
                type: "nvarchar(150)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TouristId",
                table: "TourGuideReviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TourGuideId",
                table: "TourGuideReviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TourGuideReviews_AspNetUsers_TourGuideId",
                table: "TourGuideReviews",
                column: "TourGuideId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TourGuideReviews_AspNetUsers_TouristId",
                table: "TourGuideReviews",
                column: "TouristId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TourReviews_AspNetUsers_TouristId",
                table: "TourReviews",
                column: "TouristId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TourReviews_Tours_TourId",
                table: "TourReviews",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
