 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.Models
{
    public class Libro
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int EditorialId { get; set; } = 1;
        public Editorial? Editorial { get; set; }
        [Required]
        public int Paginas { get; set; }
        [Required]
        [Column(TypeName = "Text")]
        public string Sinopsis { get; set; } = string.Empty;
        public int AnioPublicacion { get; set; }
        public string Portada { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;

        [NotMapped]
        virtual public string Autores
        {
            get
            {
                if (LibroAutores == null || LibroAutores.Count == 0)
                    return "Autor/es: " + string.Empty;
                return string.Join(", ", LibroAutores.Where(la => la.Autor != null && !la.Autor.IsDeleted).Select(la => la.Autor!.Nombre));
            }
        }

        [NotMapped]
        virtual public string Generos
        {
            get
            {
                if (LibroGeneros == null || LibroGeneros.Count == 0)
                    return "Genero/s: "+ string.Empty;
                return string.Join(", ", LibroGeneros.Where(lg => lg.Genero != null && !lg.Genero.IsDeleted).Select(lg => lg.Genero!.Nombre));
            }
        }

        // Relaciones con Autor y Genero a través de tablas intermedias
        virtual public ICollection<LibroAutor> LibroAutores { get; set; } = new List<LibroAutor>();
        virtual public ICollection<LibroGenero> LibroGeneros { get; set; } = new List<LibroGenero>();


        public override string ToString()
        {
            return Titulo;
        }

    }
}
