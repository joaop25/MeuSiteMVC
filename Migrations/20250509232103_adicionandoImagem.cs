using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuSiteMVC.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoImagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagem",
                table: "Alunos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Alunos");
        }
    }
}
