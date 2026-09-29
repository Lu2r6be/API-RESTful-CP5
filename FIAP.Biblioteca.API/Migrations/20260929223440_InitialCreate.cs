using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FIAP.Biblioteca.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    Autor = table.Column<string>(type: "TEXT", nullable: false),
                    AnoPublicacao = table.Column<int>(type: "INTEGER", nullable: false),
                    Disponivel = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Livros",
                columns: new[] { "Id", "AnoPublicacao", "Autor", "Disponivel", "Titulo" },
                values: new object[,]
                {
                    { 1, 1899, "Machado de Assis", true, "Dom Casmurro" },
                    { 2, 1965, "Frank Herbert", true, "Duna" },
                    { 3, 2008, "Robert C. Martin", false, "Clean Code" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livros");
        }
    }
}
