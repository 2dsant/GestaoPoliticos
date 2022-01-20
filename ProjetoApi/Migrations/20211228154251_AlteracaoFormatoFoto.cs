using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoApi.Migrations
{
    public partial class AlteracaoFormatoFoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Politicos_Fotos_FotoId",
                table: "Politicos");

            migrationBuilder.DropTable(
                name: "Fotos");

            migrationBuilder.DropIndex(
                name: "IX_Politicos_FotoId",
                table: "Politicos");

            migrationBuilder.DropColumn(
                name: "FotoId",
                table: "Politicos");

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Politicos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Politicos");

            migrationBuilder.AddColumn<int>(
                name: "FotoId",
                table: "Politicos",
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
                name: "IX_Politicos_FotoId",
                table: "Politicos",
                column: "FotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Politicos_Fotos_FotoId",
                table: "Politicos",
                column: "FotoId",
                principalTable: "Fotos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
