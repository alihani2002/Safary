using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class INIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         

          
            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Name);
                });

      

            migrationBuilder.CreateTable(
                name: "TourBlog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CoverImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourBlog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourBlog_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TourImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourName = table.Column<string>(type: "nvarchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourImages_Tours_TourName",
                        column: x => x.TourName,
                        principalTable: "Tours",
                        principalColumn: "Name");
                });

        

       

       

            migrationBuilder.CreateTable(
                name: "SelectedTourGuides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TouristId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TourguideId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SelectedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SelectedTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Adults = table.Column<int>(type: "int", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TourName = table.Column<string>(type: "nvarchar(150)", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_SelectedTourGuides_Tours_TourName",
                        column: x => x.TourName,
                        principalTable: "Tours",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "TourGuideReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourGuideId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TouristId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourGuideReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourGuideReviews_AspNetUsers_TourGuideId",
                        column: x => x.TourGuideId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TourGuideReviews_AspNetUsers_TouristId",
                        column: x => x.TouristId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TourReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    TouristId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourReviews_AspNetUsers_TouristId",
                        column: x => x.TouristId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TourReviews_Tours_TourName",
                        column: x => x.TourName,
                        principalTable: "Tours",
                        principalColumn: "Name");
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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SelectedTourGuides_TourName",
                table: "SelectedTourGuides",
                column: "TourName");

            migrationBuilder.CreateIndex(
                name: "IX_TourBlog_BlogId",
                table: "TourBlog",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_TourGuideReviews_TourGuideId",
                table: "TourGuideReviews",
                column: "TourGuideId");

            migrationBuilder.CreateIndex(
                name: "IX_TourGuideReviews_TouristId",
                table: "TourGuideReviews",
                column: "TouristId");

            migrationBuilder.CreateIndex(
                name: "IX_TourImages_TourName",
                table: "TourImages",
                column: "TourName");

            migrationBuilder.CreateIndex(
                name: "IX_TourReviews_TouristId",
                table: "TourReviews",
                column: "TouristId");

            migrationBuilder.CreateIndex(
                name: "IX_TourReviews_TourName",
                table: "TourReviews",
                column: "TourName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
         
            migrationBuilder.DropTable(
                name: "SelectedTourGuides");

            migrationBuilder.DropTable(
                name: "TourBlog");

            migrationBuilder.DropTable(
                name: "TourGuideReviews");

            migrationBuilder.DropTable(
                name: "TourImages");

            migrationBuilder.DropTable(
                name: "TourReviews");

            migrationBuilder.DropTable(
                name: "Tours");
         
        }
    }
}
