using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionDatosMasivos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaDevolucion",
                table: "Prestamos",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.InsertData(
                table: "Autores",
                columns: new[] { "Id", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 2, false, "Jorge Luis Borges" },
                    { 3, false, "Julio Cortázar" },
                    { 4, false, "Miguel de Cervantes" },
                    { 5, false, "Mario Vargas Llosa" },
                    { 6, false, "Juan Rulfo" },
                    { 7, false, "Adolfo Bioy Casares" },
                    { 8, false, "Antoine de Saint-Exupéry" },
                    { 9, false, "Roberto Bolaño" },
                    { 10, false, "Pablo Neruda" }
                });

            migrationBuilder.InsertData(
                table: "Carreras",
                columns: new[] { "Id", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 2, false, "Literatura" },
                    { 3, false, "Matemática" },
                    { 4, false, "Historia" },
                    { 5, false, "Filosofía" },
                    { 6, false, "Informática" },
                    { 7, false, "Derecho" },
                    { 8, false, "Medicina" },
                    { 9, false, "Arquitectura" },
                    { 10, false, "Arte" }
                });

            migrationBuilder.InsertData(
                table: "Editoriales",
                columns: new[] { "Id", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 2, false, "Emecé" },
                    { 3, false, "Alfaguara" },
                    { 4, false, "Espasa" },
                    { 5, false, "Planeta" },
                    { 6, false, "Debolsillo" },
                    { 7, false, "Seix Barral" },
                    { 8, false, "Penguin" },
                    { 9, false, "Siglo XXI" },
                    { 10, false, "Anagrama" }
                });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 2, false, "Cuento" },
                    { 3, false, "Poesía" },
                    { 4, false, "Ensayo" },
                    { 5, false, "Clásico" },
                    { 6, false, "Infantil" },
                    { 7, false, "Drama" },
                    { 8, false, "Filosofía" },
                    { 9, false, "Ficción" },
                    { 10, false, "Histórico" }
                });

            migrationBuilder.InsertData(
                table: "Libros",
                columns: new[] { "Id", "AnioPublicacion", "Descripcion", "EditorialId", "IsDeleted", "Paginas", "Portada", "Sinopsis", "Titulo" },
                values: new object[] { 7, 1955, "Novela corta", 1, false, 200, "", "Historia en Comala.", "Pedro Páramo" });

            migrationBuilder.UpdateData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaDevolucion", "FechaPrestamo" },
                values: new object[] { null, new DateTime(2025, 9, 5, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7972) });

            migrationBuilder.UpdateData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 1,
                column: "CarreraId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Dni", "Domicilio", "Email", "FechaRegistracion", "Nombre", "Observacion", "Password", "Telefono" },
                values: new object[] { "22000111", "Av. Libertad 1000", "martin@fernandez.com", new DateTime(2025, 9, 15, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7773), "Martín Fernández", "Usuario principal de prueba", "martin", "111111111" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Dni", "Domicilio", "Email", "FechaRegistracion", "IsDeleted", "Nombre", "Observacion", "Password", "Telefono", "TipoRol" },
                values: new object[,]
                {
                    { 2, "87654321", "Calle Verdadera 456", "ana@prueba.com", new DateTime(2025, 9, 15, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7777), false, "Ana Prueba", "", "abcd", "987654321", 0 },
                    { 3, "11223344", "Av. Siempre Viva 742", "carlos@test.com", new DateTime(2025, 9, 15, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7780), false, "Carlos Test", "", "pass", "111222333", 0 },
                    { 4, "55667788", "Calle Real 100", "lucia@ejemplo.com", new DateTime(2025, 9, 15, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7782), false, "Lucía Ejemplo", "", "lucia", "444555666", 0 },
                    { 5, "99887766", "Calle Nueva 321", "pedro@alumno.com", new DateTime(2025, 9, 15, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7785), false, "Pedro Alumno", "", "pedro", "777888999", 0 },
                    { 6, "33445566", "Av. Central 321", "sofia@test.com", new DateTime(2025, 9, 15, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7788), false, "Sofía Test", "", "sofia", "666555444", 0 },
                    { 7, "44556677", "Calle Demo 111", "juan@demo.com", new DateTime(2025, 9, 15, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7791), false, "Juan Demo", "", "juan", "555444333", 0 },
                    { 8, "55667799", "Av. Estudio 222", "paula@alumna.com", new DateTime(2025, 9, 15, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7794), false, "Paula Alumna", "", "paula", "444333222", 0 },
                    { 9, "66778899", "Calle 123", "diego@ejemplo.com", new DateTime(2025, 9, 15, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7796), false, "Diego Ejemplo", "", "diego", "333222111", 0 },
                    { 10, "77889900", "Av. Universidad 900", "laura@prueba.com", new DateTime(2025, 9, 15, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7799), false, "Laura Prueba", "", "laura", "222111000", 0 }
                });

            migrationBuilder.InsertData(
                table: "Ejemplares",
                columns: new[] { "Id", "Disponible", "Estado", "IsDeleted", "LibroId" },
                values: new object[] { 7, true, 0, false, 7 });

            migrationBuilder.InsertData(
                table: "LibroAutores",
                columns: new[] { "Id", "AutorId", "IsDeleted", "LibroId" },
                values: new object[] { 7, 6, false, 7 });

            migrationBuilder.InsertData(
                table: "LibroGeneros",
                columns: new[] { "Id", "GeneroId", "IsDeleted", "LibroId" },
                values: new object[] { 7, 1, false, 7 });

            migrationBuilder.InsertData(
                table: "Libros",
                columns: new[] { "Id", "AnioPublicacion", "Descripcion", "EditorialId", "IsDeleted", "Paginas", "Portada", "Sinopsis", "Titulo" },
                values: new object[,]
                {
                    { 2, 1949, "Cuentos fantásticos", 2, false, 157, "", "Colección de cuentos de Borges.", "El Aleph" },
                    { 3, 1963, "Novela experimental", 3, false, 600, "", "Historia de Horacio Oliveira.", "Rayuela" },
                    { 4, 1605, "Clásico de la literatura", 4, false, 863, "", "Aventuras de Don Quijote y Sancho.", "Don Quijote" },
                    { 5, 1923, "Poesía", 2, false, 80, "", "Poemas de Borges.", "Fervor de Buenos Aires" },
                    { 6, 1963, "Novela", 3, false, 400, "", "Historia de cadetes en Lima.", "La Ciudad y los Perros" },
                    { 8, 1940, "Ficción", 2, false, 150, "", "Novela de Bioy Casares.", "La Invención de Morel" },
                    { 9, 1943, "Clásico infantil", 5, false, 96, "", "Historia del principito.", "El Principito" },
                    { 10, 1998, "Novela", 4, false, 609, "", "Novela de Roberto Bolaño.", "Los Detectives Salvajes" }
                });

            migrationBuilder.InsertData(
                table: "UsuarioCarreras",
                columns: new[] { "Id", "CarreraId", "IsDeleted", "UsuarioId" },
                values: new object[,]
                {
                    { 2, 2, false, 2 },
                    { 3, 3, false, 3 },
                    { 4, 4, false, 4 },
                    { 5, 5, false, 5 },
                    { 6, 1, false, 6 },
                    { 7, 7, false, 7 },
                    { 8, 8, false, 8 },
                    { 9, 9, false, 9 },
                    { 10, 10, false, 10 }
                });

            migrationBuilder.InsertData(
                table: "Ejemplares",
                columns: new[] { "Id", "Disponible", "Estado", "IsDeleted", "LibroId" },
                values: new object[,]
                {
                    { 2, false, 1, false, 2 },
                    { 3, true, 0, false, 3 },
                    { 4, true, 1, false, 4 },
                    { 5, false, 0, false, 5 },
                    { 6, true, 1, false, 6 },
                    { 8, false, 4, false, 8 },
                    { 9, true, 2, false, 9 },
                    { 10, true, 0, false, 10 }
                });

            migrationBuilder.InsertData(
                table: "LibroAutores",
                columns: new[] { "Id", "AutorId", "IsDeleted", "LibroId" },
                values: new object[,]
                {
                    { 2, 2, false, 2 },
                    { 3, 3, false, 3 },
                    { 4, 4, false, 4 },
                    { 5, 2, false, 5 },
                    { 6, 5, false, 6 },
                    { 8, 7, false, 8 },
                    { 9, 8, false, 9 },
                    { 10, 9, false, 10 }
                });

            migrationBuilder.InsertData(
                table: "LibroGeneros",
                columns: new[] { "Id", "GeneroId", "IsDeleted", "LibroId" },
                values: new object[,]
                {
                    { 2, 2, false, 2 },
                    { 3, 1, false, 3 },
                    { 4, 5, false, 4 },
                    { 5, 3, false, 5 },
                    { 6, 1, false, 6 },
                    { 8, 9, false, 8 },
                    { 9, 6, false, 9 },
                    { 10, 7, false, 10 }
                });

            migrationBuilder.InsertData(
                table: "Prestamos",
                columns: new[] { "Id", "EjemplarId", "FechaDevolucion", "FechaPrestamo", "IsDeleted", "UsuarioId" },
                values: new object[,]
                {
                    { 7, 7, new DateTime(2025, 9, 5, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7992), new DateTime(2025, 8, 26, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7991), false, 3 },
                    { 2, 2, null, new DateTime(2025, 9, 7, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7979), false, 1 },
                    { 3, 3, null, new DateTime(2025, 9, 9, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7982), false, 1 },
                    { 4, 4, null, new DateTime(2025, 9, 11, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7984), false, 1 },
                    { 5, 5, null, new DateTime(2025, 9, 13, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7986), false, 1 },
                    { 6, 6, new DateTime(2025, 9, 10, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7989), new DateTime(2025, 8, 31, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7988), false, 2 },
                    { 8, 8, new DateTime(2025, 9, 13, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7995), new DateTime(2025, 9, 3, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7994), false, 4 },
                    { 9, 9, new DateTime(2025, 9, 15, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7998), new DateTime(2025, 9, 8, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(7997), false, 5 },
                    { 10, 10, new DateTime(2025, 9, 15, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(8001), new DateTime(2025, 9, 10, 16, 31, 17, 820, DateTimeKind.Local).AddTicks(8000), false, 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Autores",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Editoriales",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Editoriales",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Editoriales",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Editoriales",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Editoriales",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "LibroAutores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LibroAutores",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LibroAutores",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LibroAutores",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LibroAutores",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "LibroAutores",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "LibroAutores",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "LibroAutores",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "LibroAutores",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "LibroGeneros",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LibroGeneros",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LibroGeneros",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LibroGeneros",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LibroGeneros",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "LibroGeneros",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "LibroGeneros",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "LibroGeneros",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "LibroGeneros",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Autores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Autores",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Autores",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Autores",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Autores",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Autores",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Autores",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Autores",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Ejemplares",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ejemplares",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ejemplares",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ejemplares",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ejemplares",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ejemplares",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ejemplares",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Ejemplares",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Ejemplares",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Editoriales",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Editoriales",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Editoriales",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Editoriales",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaDevolucion",
                table: "Prestamos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaDevolucion", "FechaPrestamo" },
                values: new object[] { new DateTime(2025, 9, 4, 22, 6, 58, 86, DateTimeKind.Local).AddTicks(5166), new DateTime(2025, 8, 28, 22, 6, 58, 86, DateTimeKind.Local).AddTicks(5166) });

            migrationBuilder.UpdateData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 1,
                column: "CarreraId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Dni", "Domicilio", "Email", "FechaRegistracion", "Nombre", "Observacion", "Password", "Telefono" },
                values: new object[] { "12345678", "Calle Falsa 123", "demo@demo.com", new DateTime(2025, 8, 28, 22, 6, 58, 86, DateTimeKind.Local).AddTicks(5073), "Usuario Demo", "", "1234", "123456789" });
        }
    }
}
