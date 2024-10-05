using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryDAL.Migrations
{
    /// <inheritdoc />
    public partial class TakenBookNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.DocumentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Librarian",
                columns: table => new
                {
                    LibrarianId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Librarian", x => x.LibrarianId);
                });

            migrationBuilder.CreateTable(
                name: "PublisherCodeType",
                columns: table => new
                {
                    PublisherCodeTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherCodeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublisherCodeType", x => x.PublisherCodeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Reader",
                columns: table => new
                {
                    ReaderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfDocumentDocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reader", x => x.ReaderId);
                    table.ForeignKey(
                        name: "FK_Reader_DocumentType_TypeOfDocumentDocumentTypeId",
                        column: x => x.TypeOfDocumentDocumentTypeId,
                        principalTable: "DocumentType",
                        principalColumn: "DocumentTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublisherCodeTypeId = table.Column<int>(type: "int", nullable: false),
                    PublisherCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YearOfPublication = table.Column<int>(type: "int", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Book_PublisherCodeType_PublisherCodeTypeId",
                        column: x => x.PublisherCodeTypeId,
                        principalTable: "PublisherCodeType",
                        principalColumn: "PublisherCodeTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    AuthorsAuthorId = table.Column<int>(type: "int", nullable: false),
                    BooksBookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorsAuthorId, x.BooksBookId });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Authors_AuthorsAuthorId",
                        column: x => x.AuthorsAuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Book_BooksBookId",
                        column: x => x.BooksBookId,
                        principalTable: "Book",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TakenBook",
                columns: table => new
                {
                    TakenBookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstDayOfRent = table.Column<DateOnly>(type: "date", nullable: false),
                    ReaderId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    LastDayOfRent = table.Column<DateOnly>(type: "date", nullable: false),
                    DayOfReturn = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TakenBook", x => x.TakenBookId);
                    table.ForeignKey(
                        name: "FK_TakenBook_Book_BookId",
                        column: x => x.BookId,
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
                name: "IX_AuthorBook_BooksBookId",
                table: "AuthorBook",
                column: "BooksBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_FirstName_LastName",
                table: "Authors",
                columns: new[] { "FirstName", "LastName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_PublisherCode",
                table: "Book",
                column: "PublisherCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_PublisherCodeTypeId",
                table: "Book",
                column: "PublisherCodeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reader_DocumentNumber",
                table: "Reader",
                column: "DocumentNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reader_Email",
                table: "Reader",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reader_TypeOfDocumentDocumentTypeId",
                table: "Reader",
                column: "TypeOfDocumentDocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TakenBook_BookId",
                table: "TakenBook",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_TakenBook_ReaderId",
                table: "TakenBook",
                column: "ReaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.DropTable(
                name: "Librarian");

            migrationBuilder.DropTable(
                name: "TakenBook");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Reader");

            migrationBuilder.DropTable(
                name: "PublisherCodeType");

            migrationBuilder.DropTable(
                name: "DocumentType");
        }
    }
}
