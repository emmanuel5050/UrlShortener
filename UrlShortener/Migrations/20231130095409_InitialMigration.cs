using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TinyUrls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LongUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortCode = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinyUrls", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TinyUrls_ShortCode",
                table: "TinyUrls",
                column: "ShortCode",
                unique: true,
                filter: "[ShortCode] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TinyUrls");
        }
    }
}
