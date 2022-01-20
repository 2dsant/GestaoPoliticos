using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoApi.Migrations
{
    public partial class AdicionandoTabelaDeFotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Representantes");

            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Politicos");

            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "Cep",
                table: "Partidos");

            migrationBuilder.RenameColumn(
                name: "Rua",
                table: "Partidos",
                newName: "Sigla");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Partidos",
                newName: "Politicos");

            migrationBuilder.RenameColumn(
                name: "Cidade",
                table: "Partidos",
                newName: "Nome");

            migrationBuilder.AddColumn<int>(
                name: "TelefoneId",
                table: "Representantes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Cargo",
                table: "Politicos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "FotoId",
                table: "Politicos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "Partidos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RepresentanteId",
                table: "Partidos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Fotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Img = table.Column<byte[]>(type: "longblob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Representantes_TelefoneId",
                table: "Representantes",
                column: "TelefoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Politicos_FotoId",
                table: "Politicos",
                column: "FotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_RepresentanteId",
                table: "Partidos",
                column: "RepresentanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partidos_Representantes_RepresentanteId",
                table: "Partidos",
                column: "RepresentanteId",
                principalTable: "Representantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Politicos_Fotos_FotoId",
                table: "Politicos",
                column: "FotoId",
                principalTable: "Fotos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Representantes_Telefones_TelefoneId",
                table: "Representantes",
                column: "TelefoneId",
                principalTable: "Telefones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partidos_Representantes_RepresentanteId",
                table: "Partidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Politicos_Fotos_FotoId",
                table: "Politicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Representantes_Telefones_TelefoneId",
                table: "Representantes");

            migrationBuilder.DropTable(
                name: "Fotos");

            migrationBuilder.DropIndex(
                name: "IX_Representantes_TelefoneId",
                table: "Representantes");

            migrationBuilder.DropIndex(
                name: "IX_Politicos_FotoId",
                table: "Politicos");

            migrationBuilder.DropIndex(
                name: "IX_Partidos_RepresentanteId",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "TelefoneId",
                table: "Representantes");

            migrationBuilder.DropColumn(
                name: "FotoId",
                table: "Politicos");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "RepresentanteId",
                table: "Partidos");

            migrationBuilder.RenameColumn(
                name: "Sigla",
                table: "Partidos",
                newName: "Rua");

            migrationBuilder.RenameColumn(
                name: "Politicos",
                table: "Partidos",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Partidos",
                newName: "Cidade");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Representantes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Cargo",
                table: "Politicos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Politicos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Partidos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Cep",
                table: "Partidos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
