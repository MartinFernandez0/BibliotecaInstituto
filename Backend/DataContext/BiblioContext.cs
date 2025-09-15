using Microsoft.EntityFrameworkCore;
using Service.Enums;
using Service.Models;

namespace Backend.DataContext
{
    public class BiblioContext : DbContext
    {
        public virtual DbSet<Libro> Libros { get; set; }
        public virtual DbSet<Autor> Autores { get; set; }
        public virtual DbSet<Editorial> Editoriales { get; set; }
        public virtual DbSet<Genero> Generos { get; set; }
        public virtual DbSet<LibroAutor> LibroAutores { get; set; }
        public virtual DbSet<LibroGenero> LibroGeneros { get; set; }
        public virtual DbSet<UsuarioCarrera> UsuarioCarreras { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Prestamo> Prestamos { get; set; }
        public virtual DbSet<Carrera> Carreras { get; set; }
        public virtual DbSet<Ejemplar> Ejemplares { get; set; }

        public BiblioContext() { }

        public BiblioContext(DbContextOptions<BiblioContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();
                string? cadenaConexion = configuration.GetConnectionString("mysqlRemoto");

                optionsBuilder.UseMySql(cadenaConexion, ServerVersion.AutoDetect(cadenaConexion));

            }
        }

        //Datos Semilla

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region datos semilla masivos

            // Libros
            modelBuilder.Entity<Libro>().HasData(
                new Libro { Id = 1, Titulo = "Cien Años de Soledad", Descripcion = "Novela de realismo mágico", EditorialId = 1, Paginas = 471, Sinopsis = "Historia de la familia Buendía.", AnioPublicacion = 1967, Portada = "", IsDeleted = false },
                new Libro { Id = 2, Titulo = "El Aleph", Descripcion = "Cuentos fantásticos", EditorialId = 2, Paginas = 157, Sinopsis = "Colección de cuentos de Borges.", AnioPublicacion = 1949, Portada = "", IsDeleted = false },
                new Libro { Id = 3, Titulo = "Rayuela", Descripcion = "Novela experimental", EditorialId = 3, Paginas = 600, Sinopsis = "Historia de Horacio Oliveira.", AnioPublicacion = 1963, Portada = "", IsDeleted = false },
                new Libro { Id = 4, Titulo = "Don Quijote", Descripcion = "Clásico de la literatura", EditorialId = 4, Paginas = 863, Sinopsis = "Aventuras de Don Quijote y Sancho.", AnioPublicacion = 1605, Portada = "", IsDeleted = false },
                new Libro { Id = 5, Titulo = "Fervor de Buenos Aires", Descripcion = "Poesía", EditorialId = 2, Paginas = 80, Sinopsis = "Poemas de Borges.", AnioPublicacion = 1923, Portada = "", IsDeleted = false },
                new Libro { Id = 6, Titulo = "La Ciudad y los Perros", Descripcion = "Novela", EditorialId = 3, Paginas = 400, Sinopsis = "Historia de cadetes en Lima.", AnioPublicacion = 1963, Portada = "", IsDeleted = false },
                new Libro { Id = 7, Titulo = "Pedro Páramo", Descripcion = "Novela corta", EditorialId = 1, Paginas = 200, Sinopsis = "Historia en Comala.", AnioPublicacion = 1955, Portada = "", IsDeleted = false },
                new Libro { Id = 8, Titulo = "La Invención de Morel", Descripcion = "Ficción", EditorialId = 2, Paginas = 150, Sinopsis = "Novela de Bioy Casares.", AnioPublicacion = 1940, Portada = "", IsDeleted = false },
                new Libro { Id = 9, Titulo = "El Principito", Descripcion = "Clásico infantil", EditorialId = 5, Paginas = 96, Sinopsis = "Historia del principito.", AnioPublicacion = 1943, Portada = "", IsDeleted = false },
                new Libro { Id = 10, Titulo = "Los Detectives Salvajes", Descripcion = "Novela", EditorialId = 4, Paginas = 609, Sinopsis = "Novela de Roberto Bolaño.", AnioPublicacion = 1998, Portada = "", IsDeleted = false }
            );

            // Autores
            modelBuilder.Entity<Autor>().HasData(
                new Autor { Id = 1, Nombre = "Gabriel García Márquez", IsDeleted = false },
                new Autor { Id = 2, Nombre = "Jorge Luis Borges", IsDeleted = false },
                new Autor { Id = 3, Nombre = "Julio Cortázar", IsDeleted = false },
                new Autor { Id = 4, Nombre = "Miguel de Cervantes", IsDeleted = false },
                new Autor { Id = 5, Nombre = "Mario Vargas Llosa", IsDeleted = false },
                new Autor { Id = 6, Nombre = "Juan Rulfo", IsDeleted = false },
                new Autor { Id = 7, Nombre = "Adolfo Bioy Casares", IsDeleted = false },
                new Autor { Id = 8, Nombre = "Antoine de Saint-Exupéry", IsDeleted = false },
                new Autor { Id = 9, Nombre = "Roberto Bolaño", IsDeleted = false },
                new Autor { Id = 10, Nombre = "Pablo Neruda", IsDeleted = false }
            );

            // Editoriales
            modelBuilder.Entity<Editorial>().HasData(
                new Editorial { Id = 1, Nombre = "Sudamericana", IsDeleted = false },
                new Editorial { Id = 2, Nombre = "Emecé", IsDeleted = false },
                new Editorial { Id = 3, Nombre = "Alfaguara", IsDeleted = false },
                new Editorial { Id = 4, Nombre = "Espasa", IsDeleted = false },
                new Editorial { Id = 5, Nombre = "Planeta", IsDeleted = false },
                new Editorial { Id = 6, Nombre = "Debolsillo", IsDeleted = false },
                new Editorial { Id = 7, Nombre = "Seix Barral", IsDeleted = false },
                new Editorial { Id = 8, Nombre = "Penguin", IsDeleted = false },
                new Editorial { Id = 9, Nombre = "Siglo XXI", IsDeleted = false },
                new Editorial { Id = 10, Nombre = "Anagrama", IsDeleted = false }
            );

            // Generos
            modelBuilder.Entity<Genero>().HasData(
                new Genero { Id = 1, Nombre = "Novela", IsDeleted = false },
                new Genero { Id = 2, Nombre = "Cuento", IsDeleted = false },
                new Genero { Id = 3, Nombre = "Poesía", IsDeleted = false },
                new Genero { Id = 4, Nombre = "Ensayo", IsDeleted = false },
                new Genero { Id = 5, Nombre = "Clásico", IsDeleted = false },
                new Genero { Id = 6, Nombre = "Infantil", IsDeleted = false },
                new Genero { Id = 7, Nombre = "Drama", IsDeleted = false },
                new Genero { Id = 8, Nombre = "Filosofía", IsDeleted = false },
                new Genero { Id = 9, Nombre = "Ficción", IsDeleted = false },
                new Genero { Id = 10, Nombre = "Histórico", IsDeleted = false }
            );

            // LibroAutores
            modelBuilder.Entity<LibroAutor>().HasData(
                new LibroAutor { Id = 1, LibroId = 1, AutorId = 1, IsDeleted = false },
                new LibroAutor { Id = 2, LibroId = 2, AutorId = 2, IsDeleted = false },
                new LibroAutor { Id = 3, LibroId = 3, AutorId = 3, IsDeleted = false },
                new LibroAutor { Id = 4, LibroId = 4, AutorId = 4, IsDeleted = false },
                new LibroAutor { Id = 5, LibroId = 5, AutorId = 2, IsDeleted = false },
                new LibroAutor { Id = 6, LibroId = 6, AutorId = 5, IsDeleted = false },
                new LibroAutor { Id = 7, LibroId = 7, AutorId = 6, IsDeleted = false },
                new LibroAutor { Id = 8, LibroId = 8, AutorId = 7, IsDeleted = false },
                new LibroAutor { Id = 9, LibroId = 9, AutorId = 8, IsDeleted = false },
                new LibroAutor { Id = 10, LibroId = 10, AutorId = 9, IsDeleted = false }
            );

            // LibroGeneros
            modelBuilder.Entity<LibroGenero>().HasData(
                new LibroGenero { Id = 1, LibroId = 1, GeneroId = 1, IsDeleted = false },
                new LibroGenero { Id = 2, LibroId = 2, GeneroId = 2, IsDeleted = false },
                new LibroGenero { Id = 3, LibroId = 3, GeneroId = 1, IsDeleted = false },
                new LibroGenero { Id = 4, LibroId = 4, GeneroId = 5, IsDeleted = false },
                new LibroGenero { Id = 5, LibroId = 5, GeneroId = 3, IsDeleted = false },
                new LibroGenero { Id = 6, LibroId = 6, GeneroId = 1, IsDeleted = false },
                new LibroGenero { Id = 7, LibroId = 7, GeneroId = 1, IsDeleted = false },
                new LibroGenero { Id = 8, LibroId = 8, GeneroId = 9, IsDeleted = false },
                new LibroGenero { Id = 9, LibroId = 9, GeneroId = 6, IsDeleted = false },
                new LibroGenero { Id = 10, LibroId = 10, GeneroId = 7, IsDeleted = false }
            );

            // Usuarios (incluye Martín Fernández)
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Nombre = "Martín Fernández", Email = "martin@fernandez.com", Password = "martin", TipoRol = TipoRolEnum.Alumno, FechaRegistracion = DateTime.Now, Dni = "22000111", Domicilio = "Av. Libertad 1000", Telefono = "111111111", Observacion = "Usuario principal de prueba", IsDeleted = false },
                new Usuario { Id = 2, Nombre = "Ana Prueba", Email = "ana@prueba.com", Password = "abcd", TipoRol = TipoRolEnum.Alumno, FechaRegistracion = DateTime.Now, Dni = "87654321", Domicilio = "Calle Verdadera 456", Telefono = "987654321", Observacion = "", IsDeleted = false },
                new Usuario { Id = 3, Nombre = "Carlos Test", Email = "carlos@test.com", Password = "pass", TipoRol = TipoRolEnum.Alumno, FechaRegistracion = DateTime.Now, Dni = "11223344", Domicilio = "Av. Siempre Viva 742", Telefono = "111222333", Observacion = "", IsDeleted = false },
                new Usuario { Id = 4, Nombre = "Lucía Ejemplo", Email = "lucia@ejemplo.com", Password = "lucia", TipoRol = TipoRolEnum.Alumno, FechaRegistracion = DateTime.Now, Dni = "55667788", Domicilio = "Calle Real 100", Telefono = "444555666", Observacion = "", IsDeleted = false },
                new Usuario { Id = 5, Nombre = "Pedro Alumno", Email = "pedro@alumno.com", Password = "pedro", TipoRol = TipoRolEnum.Alumno, FechaRegistracion = DateTime.Now, Dni = "99887766", Domicilio = "Calle Nueva 321", Telefono = "777888999", Observacion = "", IsDeleted = false },
                new Usuario { Id = 6, Nombre = "Sofía Test", Email = "sofia@test.com", Password = "sofia", TipoRol = TipoRolEnum.Alumno, FechaRegistracion = DateTime.Now, Dni = "33445566", Domicilio = "Av. Central 321", Telefono = "666555444", Observacion = "", IsDeleted = false },
                new Usuario { Id = 7, Nombre = "Juan Demo", Email = "juan@demo.com", Password = "juan", TipoRol = TipoRolEnum.Alumno, FechaRegistracion = DateTime.Now, Dni = "44556677", Domicilio = "Calle Demo 111", Telefono = "555444333", Observacion = "", IsDeleted = false },
                new Usuario { Id = 8, Nombre = "Paula Alumna", Email = "paula@alumna.com", Password = "paula", TipoRol = TipoRolEnum.Alumno, FechaRegistracion = DateTime.Now, Dni = "55667799", Domicilio = "Av. Estudio 222", Telefono = "444333222", Observacion = "", IsDeleted = false },
                new Usuario { Id = 9, Nombre = "Diego Ejemplo", Email = "diego@ejemplo.com", Password = "diego", TipoRol = TipoRolEnum.Alumno, FechaRegistracion = DateTime.Now, Dni = "66778899", Domicilio = "Calle 123", Telefono = "333222111", Observacion = "", IsDeleted = false },
                new Usuario { Id = 10, Nombre = "Laura Prueba", Email = "laura@prueba.com", Password = "laura", TipoRol = TipoRolEnum.Alumno, FechaRegistracion = DateTime.Now, Dni = "77889900", Domicilio = "Av. Universidad 900", Telefono = "222111000", Observacion = "", IsDeleted = false }
            );

            // Carreras
            modelBuilder.Entity<Carrera>().HasData(
                new Carrera { Id = 1, Nombre = "Ingeniería", IsDeleted = false },
                new Carrera { Id = 2, Nombre = "Literatura", IsDeleted = false },
                new Carrera { Id = 3, Nombre = "Matemática", IsDeleted = false },
                new Carrera { Id = 4, Nombre = "Historia", IsDeleted = false },
                new Carrera { Id = 5, Nombre = "Filosofía", IsDeleted = false },
                new Carrera { Id = 6, Nombre = "Informática", IsDeleted = false },
                new Carrera { Id = 7, Nombre = "Derecho", IsDeleted = false },
                new Carrera { Id = 8, Nombre = "Medicina", IsDeleted = false },
                new Carrera { Id = 9, Nombre = "Arquitectura", IsDeleted = false },
                new Carrera { Id = 10, Nombre = "Arte", IsDeleted = false }
            );

            // UsuarioCarreras
            modelBuilder.Entity<UsuarioCarrera>().HasData(
                new UsuarioCarrera { Id = 1, UsuarioId = 1, CarreraId = 6, IsDeleted = false },
                new UsuarioCarrera { Id = 2, UsuarioId = 2, CarreraId = 2, IsDeleted = false },
                new UsuarioCarrera { Id = 3, UsuarioId = 3, CarreraId = 3, IsDeleted = false },
                new UsuarioCarrera { Id = 4, UsuarioId = 4, CarreraId = 4, IsDeleted = false },
                new UsuarioCarrera { Id = 5, UsuarioId = 5, CarreraId = 5, IsDeleted = false },
                new UsuarioCarrera { Id = 6, UsuarioId = 6, CarreraId = 1, IsDeleted = false },
                new UsuarioCarrera { Id = 7, UsuarioId = 7, CarreraId = 7, IsDeleted = false },
                new UsuarioCarrera { Id = 8, UsuarioId = 8, CarreraId = 8, IsDeleted = false },
                new UsuarioCarrera { Id = 9, UsuarioId = 9, CarreraId = 9, IsDeleted = false },
                new UsuarioCarrera { Id = 10, UsuarioId = 10, CarreraId = 10, IsDeleted = false }
            );

            // Ejemplares
            modelBuilder.Entity<Ejemplar>().HasData(
                new Ejemplar { Id = 1, LibroId = 1, Disponible = true, Estado = EstadoEnum.Exelente, IsDeleted = false },
                new Ejemplar { Id = 2, LibroId = 2, Disponible = false, Estado = EstadoEnum.MuyBueno, IsDeleted = false },
                new Ejemplar { Id = 3, LibroId = 3, Disponible = true, Estado = EstadoEnum.Exelente, IsDeleted = false },
                new Ejemplar { Id = 4, LibroId = 4, Disponible = true, Estado = EstadoEnum.MuyBueno, IsDeleted = false },
                new Ejemplar { Id = 5, LibroId = 5, Disponible = false, Estado = EstadoEnum.Exelente, IsDeleted = false },
                new Ejemplar { Id = 6, LibroId = 6, Disponible = true, Estado = EstadoEnum.MuyBueno, IsDeleted = false },
                new Ejemplar { Id = 7, LibroId = 7, Disponible = true, Estado = EstadoEnum.Exelente, IsDeleted = false },
                new Ejemplar { Id = 8, LibroId = 8, Disponible = false, Estado = EstadoEnum.Malo, IsDeleted = false },
                new Ejemplar { Id = 9, LibroId = 9, Disponible = true, Estado = EstadoEnum.Bueno, IsDeleted = false },
                new Ejemplar { Id = 10, LibroId = 10, Disponible = true, Estado = EstadoEnum.Exelente, IsDeleted = false }
            );

            modelBuilder.Entity<Prestamo>().HasData(
                // 5 préstamos activos de Martín Fernández
                new Prestamo { Id = 1, UsuarioId = 1, EjemplarId = 1, FechaPrestamo = DateTime.Now.AddDays(-10), FechaDevolucion = null },
                new Prestamo { Id = 2, UsuarioId = 1, EjemplarId = 2, FechaPrestamo = DateTime.Now.AddDays(-8), FechaDevolucion = null },
                new Prestamo { Id = 3, UsuarioId = 1, EjemplarId = 3, FechaPrestamo = DateTime.Now.AddDays(-6), FechaDevolucion = null },
                new Prestamo { Id = 4, UsuarioId = 1, EjemplarId = 4, FechaPrestamo = DateTime.Now.AddDays(-4), FechaDevolucion = null },
                new Prestamo { Id = 5, UsuarioId = 1, EjemplarId = 5, FechaPrestamo = DateTime.Now.AddDays(-2), FechaDevolucion = null },

                // Otros usuarios con préstamos devueltos
                new Prestamo { Id = 6, UsuarioId = 2, EjemplarId = 6, FechaPrestamo = DateTime.Now.AddDays(-15), FechaDevolucion = DateTime.Now.AddDays(-5) },
                new Prestamo { Id = 7, UsuarioId = 3, EjemplarId = 7, FechaPrestamo = DateTime.Now.AddDays(-20), FechaDevolucion = DateTime.Now.AddDays(-10) },
                new Prestamo { Id = 8, UsuarioId = 4, EjemplarId = 8, FechaPrestamo = DateTime.Now.AddDays(-12), FechaDevolucion = DateTime.Now.AddDays(-2) },
                new Prestamo { Id = 9, UsuarioId = 5, EjemplarId = 9, FechaPrestamo = DateTime.Now.AddDays(-7), FechaDevolucion = DateTime.Now },
                new Prestamo { Id = 10, UsuarioId = 6, EjemplarId = 10, FechaPrestamo = DateTime.Now.AddDays(-5), FechaDevolucion = DateTime.Now }
            );


            // (este código no lo habilitamos todavía porque es cuando agregamos un campo Eliminado a las tablas y modificamos los
            // ApiControllers para que al mandar a eliminar solo cambien este campo y lo pongan en verdadero, esta configuración de
            // eliminación hace que el sistema ignore los registros que tengan el eliminado en verdadero, así que hace que en
            // apariencia y funcionalidad esté "eliminado", pero van a seguir estando ahí para que podamos observar las eliminaciones que hubo)
            modelBuilder.Entity<Autor>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<Carrera>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Editorial>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Ejemplar>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Genero>().HasQueryFilter(g => !g.IsDeleted);
            modelBuilder.Entity<Libro>().HasQueryFilter(l => !l.IsDeleted);
            modelBuilder.Entity<LibroAutor>().HasQueryFilter(l => !l.IsDeleted);
            modelBuilder.Entity<LibroGenero>().HasQueryFilter(l => !l.IsDeleted);
            modelBuilder.Entity<Prestamo>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Usuario>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<UsuarioCarrera>().HasQueryFilter(u => !u.IsDeleted);

            #endregion

        }
    }
}