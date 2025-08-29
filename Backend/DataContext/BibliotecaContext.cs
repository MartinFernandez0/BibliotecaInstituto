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
                new Libro { Id = 1, Titulo = "Cien Años de Soledad", Descripcion = "Novela de realismo mágico", EditorialId = 1, Paginas = 471, Sinopsis = "Historia de la familia Buendía.", AnioPublicacion = 1967, Portada = "", IsDeleted = false }
            );

            // Autores
            modelBuilder.Entity<Autor>().HasData(
                new Autor { Id = 1, Nombre = "Gabriel García Márquez", IsDeleted = false }
            );

            // Editoriales
            modelBuilder.Entity<Editorial>().HasData(
                new Editorial { Id = 1, Nombre = "Sudamericana", IsDeleted = false }
            );

            // Generos
            modelBuilder.Entity<Genero>().HasData(
                new Genero { Id = 1, Nombre = "Novela", IsDeleted = false }
            );

            // LibroAutores
            modelBuilder.Entity<LibroAutor>().HasData(
                new LibroAutor { Id = 1, LibroId = 1, AutorId = 1, IsDeleted = false }
            );

            // LibroGeneros
            modelBuilder.Entity<LibroGenero>().HasData(
                new LibroGenero { Id = 1, LibroId = 1, GeneroId = 1, IsDeleted = false }
            );

            // Usuarios
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Nombre = "Usuario Demo", Email = "demo@demo.com", Password = "1234", TipoRol = TipoRolEnum.Alumno, FechaRegistracion = DateTime.Now, Dni = "12345678", Domicilio = "Calle Falsa 123", Telefono = "123456789", Observacion = "", IsDeleted = false }
            );

            // Carreras
            modelBuilder.Entity<Carrera>().HasData(
                new Carrera { Id = 1, Nombre = "Ingeniería", IsDeleted = false }
            );

            // UsuarioCarreras
            modelBuilder.Entity<UsuarioCarrera>().HasData(
                new UsuarioCarrera { Id = 1, UsuarioId = 1, CarreraId = 1, IsDeleted = false }
            );

            // Ejemplares
            modelBuilder.Entity<Ejemplar>().HasData(
                new Ejemplar { Id = 1, LibroId = 1, Disponible = true, Estado = EstadoEnum.Exelente, IsDeleted = false }
            );

            // Prestamos
            modelBuilder.Entity<Prestamo>().HasData(
                new Prestamo { Id = 1, UsuarioId = 1, EjemplarId = 1, FechaPrestamo = DateTime.Now, FechaDevolucion = DateTime.Now.AddDays(7), IsDeleted = false }
            );

            #endregion

            #region definición de filtros de eliminación
            // (este código no lo habilitamos todavía porque es cuando agregamos un campo Eliminado a las tablas y modificamos los
            // ApiControllers para que al mandar a eliminar solo cambien este campo y lo pongan en verdadero, esta configuración de
            // eliminación hace que el sistema ignore los registros que tengan el eliminado en verdadero, así que hace que en
            // apariencia y funcionalidad esté "eliminado", pero van a seguir estando ahí para que podamos observar las eliminaciones que hubo)
            modelBuilder.Entity<Autor>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Carrera>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Editorial>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Ejemplar>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Genero>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Libro>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<LibroAutor>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<LibroGenero>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Prestamo>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Usuario>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<UsuarioCarrera>().HasQueryFilter(m => !m.IsDeleted);

            #endregion
        }
    }
}
