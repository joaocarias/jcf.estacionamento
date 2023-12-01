using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Jcf.Estacionamento.Api.Migrations
{
    /// <inheritdoc />
    public partial class Inicial_migracao_usuario_tabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Senha = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataRemocao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UsuarioCriacaoId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    UsuarioAtualizacaoId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Usuarios_UsuarioAtualizacaoId",
                        column: x => x.UsuarioAtualizacaoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuarios_Usuarios_UsuarioCriacaoId",
                        column: x => x.UsuarioCriacaoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCriacao", "DataRemocao", "Email", "Nome", "Role", "Senha", "UsuarioAtualizacaoId", "UsuarioCriacaoId" },
                values: new object[,]
                {
                    { new Guid("08dbd59a-2683-4c67-8e16-689ba2648545"), true, null, new DateTime(2023, 12, 1, 12, 59, 43, 199, DateTimeKind.Utc).AddTicks(4450), null, "admin@email.com", "Administrador Usuário", "ADMIN", "4BADAEE57FED5610012A296273158F5F", null, null },
                    { new Guid("08dbdc08-56e1-4e90-805f-db29361e075e"), true, null, new DateTime(2023, 12, 1, 12, 59, 43, 199, DateTimeKind.Utc).AddTicks(4512), null, "basico@email.com", "Básico Usuário", "BASICO", "4BADAEE57FED5610012A296273158F5F", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_UsuarioAtualizacaoId",
                table: "Usuarios",
                column: "UsuarioAtualizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_UsuarioCriacaoId",
                table: "Usuarios",
                column: "UsuarioCriacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
