using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoApi.Migrations
{
    public partial class AlteracaoTelefone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Representantes_Telefones_TelefoneId",
                table: "Representantes");

            migrationBuilder.DropIndex(
                name: "IX_Representantes_TelefoneId",
                table: "Representantes");

            migrationBuilder.DropColumn(
                name: "TelefoneId",
                table: "Representantes");

            migrationBuilder.AddColumn<int>(
                name: "RepresentanteId",
                table: "Telefones",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Telefones_RepresentanteId",
                table: "Telefones",
                column: "RepresentanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Telefones_Representantes_RepresentanteId",
                table: "Telefones",
                column: "RepresentanteId",
                principalTable: "Representantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Telefones_Representantes_RepresentanteId",
                table: "Telefones");

            migrationBuilder.DropIndex(
                name: "IX_Telefones_RepresentanteId",
                table: "Telefones");

            migrationBuilder.DropColumn(
                name: "RepresentanteId",
                table: "Telefones");

            migrationBuilder.AddColumn<int>(
                name: "TelefoneId",
                table: "Representantes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Representantes_TelefoneId",
                table: "Representantes",
                column: "TelefoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Representantes_Telefones_TelefoneId",
                table: "Representantes",
                column: "TelefoneId",
                principalTable: "Telefones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
