﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fiap.Api.AspNet.Migrations
{
    public partial class InicialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FiapCategoria",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCategoria = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiapCategoria", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "FiapMarca",
                columns: table => new
                {
                    MarcaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeMarca = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiapMarca", x => x.MarcaId);
                });

            migrationBuilder.CreateTable(
                name: "FiapUsuario",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeUsuario = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Regra = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiapUsuario", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "FiapProduto",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sku = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Caracteristicas = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    DataLancamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    MarcaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiapProduto", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_FiapProduto_FiapCategoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "FiapCategoria",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FiapProduto_FiapMarca_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "FiapMarca",
                        principalColumn: "MarcaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FiapCategoria",
                columns: new[] { "CategoriaId", "NomeCategoria" },
                values: new object[,]
                {
                    { 1, "Smartphone" },
                    { 2, "Televisor" },
                    { 3, "Notebook" },
                    { 4, "Tablet" }
                });

            migrationBuilder.InsertData(
                table: "FiapMarca",
                columns: new[] { "MarcaId", "NomeMarca" },
                values: new object[,]
                {
                    { 1, "Apple" },
                    { 2, "Samsung" },
                    { 3, "Google" },
                    { 4, "Xiaomi" }
                });

            migrationBuilder.InsertData(
                table: "FiapUsuario",
                columns: new[] { "UsuarioId", "NomeUsuario", "Regra", "Senha" },
                values: new object[,]
                {
                    { 1, "Admin Senior", "Senior", "123456" },
                    { 2, "Admin Pleno", "Pleno", "123456" },
                    { 3, "Admin Junior", "Junior", "123456" }
                });

            migrationBuilder.InsertData(
                table: "FiapProduto",
                columns: new[] { "ProdutoId", "Caracteristicas", "CategoriaId", "DataLancamento", "Descricao", "MarcaId", "Nome", "Preco", "Sku" },
                values: new object[] { 1, "", 1, new DateTime(2021, 9, 11, 9, 29, 0, 472, DateTimeKind.Local).AddTicks(1241), "Apple iPhone 12", 1, "iPhone 12", 5000m, "SKUIPH12" });

            migrationBuilder.CreateIndex(
                name: "IX_FiapProduto_CategoriaId",
                table: "FiapProduto",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_FiapProduto_MarcaId",
                table: "FiapProduto",
                column: "MarcaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FiapProduto");

            migrationBuilder.DropTable(
                name: "FiapUsuario");

            migrationBuilder.DropTable(
                name: "FiapCategoria");

            migrationBuilder.DropTable(
                name: "FiapMarca");
        }
    }
}
