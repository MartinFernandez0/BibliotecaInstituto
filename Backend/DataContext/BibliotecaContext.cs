using Microsoft.EntityFrameworkCore;
using Service.Enums;
using Service.Models;

namespace Backend.DataContext
{
    public class BibliotecaContext: DbContext
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

        public BibliotecaContext(){ }

        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options){ }

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
            #region datos semilla simples

            // Libros
            modelBuilder.Entity<Libro>().HasData(
                new Libro { Id = 1, Titulo = "Cien Años de Soledad", Descripcion = "Novela de realismo mágico", EditorialId = 1, Paginas = 471, Sinopsis = "Historia de la familia Buendía.", AnioPublicacion = 1967, Portada = "", IsDeleted = false },
                new Libro { Id = 2, Titulo = "El Aleph", Descripcion = "Cuentos fantásticos", EditorialId = 2, Paginas = 157, Sinopsis = "Colección de cuentos de Borges.", AnioPublicacion = 1949, Portada = "", IsDeleted = false },
                new Libro { Id = 3, Titulo = "Rayuela", Descripcion = "Novela experimental", EditorialId = 3, Paginas = 600, Sinopsis = "Historia de Horacio Oliveira.", AnioPublicacion = 1963, Portada = "", IsDeleted = false },
                new Libro { Id = 4, Titulo = "Don Quijote", Descripcion = "Clásico de la literatura", EditorialId = 4, Paginas = 863, Sinopsis = "Aventuras de Don Quijote y Sancho.", AnioPublicacion = 1605, Portada = "", IsDeleted = false },
                new Libro { Id = 5, Titulo = "Fervor de Buenos Aires", Descripcion = "Poesía", EditorialId = 2, Paginas = 80, Sinopsis = "Poemas de Borges.", AnioPublicacion = 1923, Portada = "", IsDeleted = false }
            );

            // Autores
            modelBuilder.Entity<Autor>().HasData(
                new Autor { Id = 1, Nombre = "Gabriel García Márquez", IsDeleted = false },
                new Autor { Id = 2, Nombre = "Jorge Luis Borges", IsDeleted = false },
                new Autor { Id = 3, Nombre = "Julio Cortázar", IsDeleted = false },
                new Autor { Id = 4, Nombre = "Miguel de Cervantes", IsDeleted = false },
                new Autor { Id = 5, Nombre = "Mario Vargas Llosa", IsDeleted = false }
            );

            // Editoriales
            modelBuilder.Entity<Editorial>().HasData(
                new Editorial { Id = 1, Nombre = "Sudamericana", IsDeleted = false },
                new Editorial { Id = 2, Nombre = "Emecé", IsDeleted = false },
                new Editorial { Id = 3, Nombre = "Alfaguara", IsDeleted = false },
                new Editorial { Id = 4, Nombre = "Espasa", IsDeleted = false },
                new Editorial { Id = 5, Nombre = "Planeta", IsDeleted = false }
            );

            // Generos
            modelBuilder.Entity<Genero>().HasData(
                new Genero { Id = 1, Nombre = "Novela", IsDeleted = false },
                new Genero { Id = 2, Nombre = "Cuento", IsDeleted = false },
                new Genero { Id = 3, Nombre = "Poesía", IsDeleted = false },
                new Genero { Id = 4, Nombre = "Ensayo", IsDeleted = false },
                new Genero { Id = 5, Nombre = "Clásico", IsDeleted = false }
            );

            // LibroAutores
            modelBuilder.Entity<LibroAutor>().HasData(
                new LibroAutor { Id = 1, LibroId = 1, AutorId = 1, IsDeleted = false },
                new LibroAutor { Id = 2, LibroId = 2, AutorId = 2, IsDeleted = false },
                new LibroAutor { Id = 3, LibroId = 3, AutorId = 3, IsDeleted = false },
                new LibroAutor { Id = 4, LibroId = 4, AutorId = 4, IsDeleted = false },
                new LibroAutor { Id = 5, LibroId = 5, AutorId = 2, IsDeleted = false }
            );

            // LibroGeneros
            modelBuilder.Entity<LibroGenero>().HasData(
                new LibroGenero { Id = 1, LibroId = 1, GeneroId = 1, IsDeleted = false },
                new LibroGenero { Id = 2, LibroId = 2, GeneroId = 2, IsDeleted = false },
                new LibroGenero { Id = 3, LibroId = 3, GeneroId = 1, IsDeleted = false },
                new LibroGenero { Id = 4, LibroId = 4, GeneroId = 5, IsDeleted = false },
                new LibroGenero { Id = 5, LibroId = 5, GeneroId = 3, IsDeleted = false }
            );

            // Usuarios
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Nombre = "Usuario Demo", Email = "demo@demo.com", Password = "1234", TipoRol = TipoRolEnum.Alumno, FechaRegistracion = DateTime.Now, Dni = "12345678", Domicilio = "Calle Falsa 123", Telefono = "123456789", Observacion = "", IsDeleted = false },
                new Usuario { Id = 2, Nombre = "Ana Prueba", Email = "ana@prueba.com", Password = "abcd", TipoRol = TipoRolEnum.Alumno, FechaRegistracion = DateTime.Now, Dni = "87654321", Domicilio = "Calle Verdadera 456", Telefono = "987654321", Observacion = "", IsDeleted = false },
                new Usuario { Id = 3, Nombre = "Carlos Test", Email = "carlos@test.com", Password = "pass", TipoRol = TipoRolEnum.Alumno, FechaRegistracion = DateTime.Now, Dni = "11223344", Domicilio = "Av. Siempre Viva 742", Telefono = "111222333", Observacion = "", IsDeleted = false },
                new Usuario { Id = 4, Nombre = "Lucía Ejemplo", Email = "lucia@ejemplo.com", Password = "lucia", TipoRol = TipoRolEnum.Alumno, FechaRegistracion = DateTime.Now, Dni = "55667788", Domicilio = "Calle Real 100", Telefono = "444555666", Observacion = "", IsDeleted = false },
                new Usuario { Id = 5, Nombre = "Pedro Alumno", Email = "pedro@alumno.com", Password = "pedro", TipoRol = TipoRolEnum.Alumno, FechaRegistracion = DateTime.Now, Dni = "99887766", Domicilio = "Calle Nueva 321", Telefono = "777888999", Observacion = "", IsDeleted = false }
            );

            // Carreras
            modelBuilder.Entity<Carrera>().HasData(
                new Carrera { Id = 1, Nombre = "Ingeniería", IsDeleted = false },
                new Carrera { Id = 2, Nombre = "Literatura", IsDeleted = false },
                new Carrera { Id = 3, Nombre = "Matemática", IsDeleted = false },
                new Carrera { Id = 4, Nombre = "Historia", IsDeleted = false },
                new Carrera { Id = 5, Nombre = "Filosofía", IsDeleted = false }
            );

            // UsuarioCarreras
            modelBuilder.Entity<UsuarioCarrera>().HasData(
                new UsuarioCarrera { Id = 1, UsuarioId = 1, CarreraId = 1, IsDeleted = false },
                new UsuarioCarrera { Id = 2, UsuarioId = 2, CarreraId = 2, IsDeleted = false },
                new UsuarioCarrera { Id = 3, UsuarioId = 3, CarreraId = 3, IsDeleted = false },
                new UsuarioCarrera { Id = 4, UsuarioId = 4, CarreraId = 4, IsDeleted = false },
                new UsuarioCarrera { Id = 5, UsuarioId = 5, CarreraId = 5, IsDeleted = false }
            );

            // Ejemplares
            modelBuilder.Entity<Ejemplar>().HasData(
                new Ejemplar { Id = 1, LibroId = 1, Disponible = true, Estado = EstadoEnum.Exelente, IsDeleted = false },
                new Ejemplar { Id = 2, LibroId = 2, Disponible = false, Estado = EstadoEnum.MuyBueno, IsDeleted = false },
                new Ejemplar { Id = 3, LibroId = 3, Disponible = true, Estado = EstadoEnum.Exelente, IsDeleted = false },
                new Ejemplar { Id = 4, LibroId = 4, Disponible = true, Estado = EstadoEnum.MuyBueno, IsDeleted = false },
                new Ejemplar { Id = 5, LibroId = 5, Disponible = false, Estado = EstadoEnum.Exelente, IsDeleted = false }
            );

            // Prestamos
            modelBuilder.Entity<Prestamo>().HasData(
                new Prestamo { Id = 1, UsuarioId = 1, EjemplarId = 1, FechaPrestamo = DateTime.Now, FechaDevolucion = DateTime.Now.AddDays(7), IsDeleted = false },
                new Prestamo { Id = 2, UsuarioId = 2, EjemplarId = 2, FechaPrestamo = DateTime.Now, FechaDevolucion = DateTime.Now.AddDays(10), IsDeleted = false },
                new Prestamo { Id = 3, UsuarioId = 3, EjemplarId = 3, FechaPrestamo = DateTime.Now, FechaDevolucion = DateTime.Now.AddDays(5), IsDeleted = false },
                new Prestamo { Id = 4, UsuarioId = 4, EjemplarId = 4, FechaPrestamo = DateTime.Now, FechaDevolucion = DateTime.Now.AddDays(14), IsDeleted = false },
                new Prestamo { Id = 5, UsuarioId = 5, EjemplarId = 5, FechaPrestamo = DateTime.Now, FechaDevolucion = DateTime.Now.AddDays(3), IsDeleted = false }
            );

            #endregion

            #region definición de filtros de eliminación
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
