using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jcf.Estacionamento.Api.Migrations
{
    /// <inheritdoc />
    public partial class Estacionamento_seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Estacionamentos",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Estacionamentos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCriacao", "DataRemocao", "Nome", "TotalVagasCarro", "TotalVagasGrandes", "TotalVagasMoto", "UsuarioAtualizacaoId", "UsuarioCriacaoId" },
                values: new object[] { new Guid("08dbe21f-7354-4a2a-8b53-63cc10662e98"), true, null, new DateTime(2023, 12, 4, 0, 56, 27, 55, DateTimeKind.Utc).AddTicks(2043), null, "Estacionamento Principal", 10, 3, 30, null, null });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("08dbd59a-2683-4c67-8e16-689ba2648545"),
                column: "DataCriacao",
                value: new DateTime(2023, 12, 4, 0, 56, 27, 55, DateTimeKind.Utc).AddTicks(216));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("08dbdc08-56e1-4e90-805f-db29361e075e"),
                column: "DataCriacao",
                value: new DateTime(2023, 12, 4, 0, 56, 27, 55, DateTimeKind.Utc).AddTicks(282));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Estacionamentos",
                keyColumn: "Id",
                keyValue: new Guid("08dbe21f-7354-4a2a-8b53-63cc10662e98"));

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Estacionamentos",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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
        }
    }
}
