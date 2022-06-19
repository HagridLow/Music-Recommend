using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class DataCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlbumRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumRatings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlbumStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Status = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpotifyAlbums",
                columns: table => new
                {
                    ID = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Artist = table.Column<string>(type: "TEXT", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    TotalTracks = table.Column<int>(type: "INTEGER", nullable: false),
                    ReleaseDate = table.Column<string>(type: "TEXT", nullable: true),
                    Tracks = table.Column<string>(type: "TEXT", nullable: true),
                    AlbumRatingId = table.Column<int>(type: "INTEGER", nullable: false),
                    AlbumStatusId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpotifyAlbums", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SpotifyAlbums_AlbumRatings_AlbumRatingId",
                        column: x => x.AlbumRatingId,
                        principalTable: "AlbumRatings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpotifyAlbums_AlbumStatuses_AlbumStatusId",
                        column: x => x.AlbumStatusId,
                        principalTable: "AlbumStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpotifyAlbums_AlbumRatingId",
                table: "SpotifyAlbums",
                column: "AlbumRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_SpotifyAlbums_AlbumStatusId",
                table: "SpotifyAlbums",
                column: "AlbumStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpotifyAlbums");

            migrationBuilder.DropTable(
                name: "AlbumRatings");

            migrationBuilder.DropTable(
                name: "AlbumStatuses");
        }
    }
}
