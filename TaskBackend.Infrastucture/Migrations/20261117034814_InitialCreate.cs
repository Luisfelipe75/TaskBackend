using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskBackend.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    States = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Responsible = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreatedAt", "Description", "DueDate", "Responsible", "States", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Instalar .NET SDK, Visual Studio, SQL Server, etc.", new DateTime(2026, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juan Pérez", "Pendiente", "Configurar entorno de desarrollo", new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2026, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Definir esquemas de tablas para tareas, usuarios.", new DateTime(2026, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "María García", "EnCurso", "Diseñar base de datos", new DateTime(2026, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2026, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Desarrollar middleware de autenticación y lógica de login.", new DateTime(2026, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Carlos Ruíz", "Hecha", "Implementar autenticación JWT", new DateTime(2026, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_DueDate",
                table: "Tasks",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Responsible",
                table: "Tasks",
                column: "Responsible");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_States",
                table: "Tasks",
                column: "States");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
