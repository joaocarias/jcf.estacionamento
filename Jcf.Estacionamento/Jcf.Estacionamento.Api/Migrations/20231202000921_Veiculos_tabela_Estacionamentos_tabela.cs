using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jcf.Estacionamento.Api.Migrations
{
    /// <inheritdoc />
    public partial class Veiculos_tabela_Estacionamentos_tabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estacionamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalVagasMoto = table.Column<int>(type: "int", nullable: false),
                    TotalVagasCarro = table.Column<int>(type: "int", nullable: false),
                    TotalVagasGrandes = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataRemocao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UsuarioCriacaoId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    UsuarioAtualizacaoId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estacionamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estacionamentos_Usuarios_UsuarioAtualizacaoId",
                        column: x => x.UsuarioAtualizacaoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Estacionamentos_Usuarios_UsuarioCriacaoId",
                        column: x => x.UsuarioCriacaoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Placa = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataRemocao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UsuarioCriacaoId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    UsuarioAtualizacaoId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veiculos_Usuarios_UsuarioAtualizacaoId",
                        column: x => x.UsuarioAtualizacaoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Veiculos_Usuarios_UsuarioCriacaoId",
                        column: x => x.UsuarioCriacaoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EstacionamentoVeiculo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EstacionamentoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    VeiculoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Ocupacao = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataRemocao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UsuarioCriacaoId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    UsuarioAtualizacaoId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstacionamentoVeiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstacionamentoVeiculo_Estacionamentos_EstacionamentoId",
                        column: x => x.EstacionamentoId,
                        principalTable: "Estacionamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstacionamentoVeiculo_Usuarios_UsuarioAtualizacaoId",
                        column: x => x.UsuarioAtualizacaoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EstacionamentoVeiculo_Usuarios_UsuarioCriacaoId",
                        column: x => x.UsuarioCriacaoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EstacionamentoVeiculo_Veiculos_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("08dbd59a-2683-4c67-8e16-689ba2648545"),
                column: "DataCriacao",
                value: new DateTime(2023, 12, 2, 0, 9, 20, 931, DateTimeKind.Utc).AddTicks(8337));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("08dbdc08-56e1-4e90-805f-db29361e075e"),
                column: "DataCriacao",
                value: new DateTime(2023, 12, 2, 0, 9, 20, 931, DateTimeKind.Utc).AddTicks(8552));

            migrationBuilder.CreateIndex(
                name: "IX_Estacionamentos_UsuarioAtualizacaoId",
                table: "Estacionamentos",
                column: "UsuarioAtualizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estacionamentos_UsuarioCriacaoId",
                table: "Estacionamentos",
                column: "UsuarioCriacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_EstacionamentoVeiculo_EstacionamentoId",
                table: "EstacionamentoVeiculo",
                column: "EstacionamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_EstacionamentoVeiculo_UsuarioAtualizacaoId",
                table: "EstacionamentoVeiculo",
                column: "UsuarioAtualizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_EstacionamentoVeiculo_UsuarioCriacaoId",
                table: "EstacionamentoVeiculo",
                column: "UsuarioCriacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_EstacionamentoVeiculo_VeiculoId",
                table: "EstacionamentoVeiculo",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_UsuarioAtualizacaoId",
                table: "Veiculos",
                column: "UsuarioAtualizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_UsuarioCriacaoId",
                table: "Veiculos",
                column: "UsuarioCriacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstacionamentoVeiculo");

            migrationBuilder.DropTable(
                name: "Estacionamentos");

            migrationBuilder.DropTable(
                name: "Veiculos");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("08dbd59a-2683-4c67-8e16-689ba2648545"),
                column: "DataCriacao",
                value: new DateTime(2023, 12, 1, 12, 59, 43, 199, DateTimeKind.Utc).AddTicks(4450));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("08dbdc08-56e1-4e90-805f-db29361e075e"),
                column: "DataCriacao",
                value: new DateTime(2023, 12, 1, 12, 59, 43, 199, DateTimeKind.Utc).AddTicks(4512));
        }
    }
}
