using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewApi.Persistence.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ImageName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUser = table.Column<int>(type: "INTEGER", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RestaurantReviewed = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewScoreCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewScoreCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Score = table.Column<decimal>(type: "TEXT", nullable: false),
                    ReviewId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewScores_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ReviewScoreCategories",
                columns: new[] { "Id", "Description" },
                values: new object[] { 1, "Taste" });

            migrationBuilder.InsertData(
                table: "ReviewScoreCategories",
                columns: new[] { "Id", "Description" },
                values: new object[] { 2, "Texture" });

            migrationBuilder.InsertData(
                table: "ReviewScoreCategories",
                columns: new[] { "Id", "Description" },
                values: new object[] { 3, "VisualPresentation" });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewScores_ReviewId",
                table: "ReviewScores",
                column: "ReviewId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewScoreCategories");

            migrationBuilder.DropTable(
                name: "ReviewScores");

            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
