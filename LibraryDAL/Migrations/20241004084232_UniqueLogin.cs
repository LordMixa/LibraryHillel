using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryDAL.Migrations
{
    /// <inheritdoc />
    public partial class UniqueLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Reader",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Librarian",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Reader_Login",
                table: "Reader",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Librarian_Login",
                table: "Librarian",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reader_Login",
                table: "Reader");

            migrationBuilder.DropIndex(
                name: "IX_Librarian_Login",
                table: "Librarian");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Reader",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Librarian",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
