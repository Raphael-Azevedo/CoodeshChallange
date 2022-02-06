using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SpaceFlightNews.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: false),
                    NewsSite = table.Column<string>(nullable: false),
                    Summary = table.Column<string>(maxLength: 1000, nullable: false),
                    PublishedAt = table.Column<string>(nullable: true, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<string>(nullable: true),
                    Featured = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Provider = table.Column<string>(nullable: false),
                    ArticleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Launches",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Provider = table.Column<string>(nullable: false),
                    ArticleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Launches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Launches_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_ArticleId",
                table: "Events",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Launches_ArticleId",
                table: "Launches",
                column: "ArticleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Launches");

            migrationBuilder.DropTable(
                name: "Articles");
        }
    }
}
