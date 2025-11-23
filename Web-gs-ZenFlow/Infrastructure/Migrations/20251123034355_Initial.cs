using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_gs_ZenFlow.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_GS_USUARIO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NomeCompleto = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Cpf = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    IsGestor = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    Ativo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GS_USUARIO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_GS_REGISTRO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NivelEstresse = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Observacoes = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true),
                    Data = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    Ativo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GS_REGISTRO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_GS_REGISTRO_T_GS_USUARIO_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "T_GS_USUARIO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_GS_REGISTRO_UsuarioId",
                table: "T_GS_REGISTRO",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_T_GS_USUARIO_Cpf",
                table: "T_GS_USUARIO",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_GS_USUARIO_Email",
                table: "T_GS_USUARIO",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_GS_REGISTRO");

            migrationBuilder.DropTable(
                name: "T_GS_USUARIO");
        }
    }
}
