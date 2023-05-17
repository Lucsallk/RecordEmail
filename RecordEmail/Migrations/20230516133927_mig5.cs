using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordEmail.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Atos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Numero = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataCadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataAto = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataPublicacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Disponivel = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PalavrasChave = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdUsuarioCriador = table.Column<int>(type: "int", nullable: false),
                    Ementa = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAtivo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IdTipoAto = table.Column<int>(type: "int", nullable: false),
                    IdIniciativa = table.Column<int>(type: "int", nullable: true),
                    DataRepublicacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MensagemGovernamental = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroOrigem = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Assinatura = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdTipoOrigem = table.Column<int>(type: "int", nullable: true),
                    Inteiro = table.Column<int>(type: "int", nullable: true),
                    IdSituacao = table.Column<int>(type: "int", nullable: true),
                    IdParecer = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atos_Atos_IdParecer",
                        column: x => x.IdParecer,
                        principalTable: "Atos",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Atos_IdParecer",
                table: "Atos",
                column: "IdParecer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atos");
        }
    }
}
