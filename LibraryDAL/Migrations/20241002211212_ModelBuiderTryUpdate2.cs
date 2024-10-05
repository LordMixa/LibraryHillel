using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryDAL.Migrations
{
    /// <inheritdoc />
    public partial class ModelBuiderTryUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reader_Email",
                table: "Reader");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Reader",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Reader",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Reader_Email",
                table: "Reader",
                column: "Email",
                unique: true);
        }
    }
}
