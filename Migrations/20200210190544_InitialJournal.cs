using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotJournal.Migrations
{
    public partial class InitialJournal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(maxLength: 450, nullable: false),
                    title = table.Column<string>(nullable: false),
                    colour = table.Column<string>(maxLength: 6, nullable: true),
                    dateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getDate()"),
                    dateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pages",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(nullable: false),
                    pageNum = table.Column<string>(nullable: false),
                    content = table.Column<string>(nullable: false),
                    bookId = table.Column<int>(nullable: false),
                    dateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getDate()"),
                    dateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pages", x => x.id);
                    table.ForeignKey(
                        name: "FK_pages_books_bookId",
                        column: x => x.bookId,
                        principalTable: "books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pages_bookId",
                table: "pages",
                column: "bookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pages");

            migrationBuilder.DropTable(
                name: "books");
        }
    }
}
