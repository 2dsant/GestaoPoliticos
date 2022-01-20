using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoApi.Migrations
{
    public partial class AlteracaoPartido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Politicos",
                table: "Partidos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Politicos",
                table: "Partidos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
