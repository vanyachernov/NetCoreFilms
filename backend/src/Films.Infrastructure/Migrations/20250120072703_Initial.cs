using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Films.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "films",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    film_description = table.Column<string>(type: "text", nullable: false),
                    film_director = table.Column<string>(type: "text", nullable: false),
                    film_genre = table.Column<string>(type: "text", nullable: false),
                    film_rating = table.Column<int>(type: "integer", nullable: false),
                    film_release_year = table.Column<int>(type: "integer", nullable: false),
                    film_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_films", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "films");
        }
    }
}
