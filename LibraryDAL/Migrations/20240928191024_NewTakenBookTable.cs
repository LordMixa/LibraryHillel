using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryDAL.Migrations
{
    /// <inheritdoc />
    public partial class NewTakenBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TakenBook",
                columns: table => new
                {
                    TakenBookId = table.Column<int>(type: "int", nullable: false),
                    FirstDayOfRent = table.Column<DateOnly>(type: "date", nullable: false),
                    ReaderId = table.Column<int>(type: "int", nullable: false),
                    LastDayOfRent = table.Column<DateOnly>(type: "date", nullable: false),
                    DayOfReturn = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TakenBook", x => x.TakenBookId);
                    table.ForeignKey(
                        name: "FK_TakenBook_Book_TakenBookId",
                        column: x => x.TakenBookId,
                        principalTable: "Book",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TakenBook_Reader_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "Reader",
                        principalColumn: "ReaderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TakenBook_ReaderId",
                table: "TakenBook",
                column: "ReaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TakenBook");
        }
    }
}
