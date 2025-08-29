using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Class
{
    public static class ApiEndpoint
    {
        public static string Libros { get; set; } = "libros";
        public static string Autores { get; set; } = "autores";
        public static string Editoriales { get; set; } = "editoriales";
        public static string Generos { get; set; } = "generos";
        public static string LibroAutores { get; set; } = "libroautores";
        public static string LibroGeneros { get; set; } = "librogeneros";
        public static string UsuarioCarreras { get; set; } = "usuariocarreras";
        public static string Usuarios { get; set; } = "usuarios";
        public static string Prestamos { get; set; } = "prestamos";
        public static string Carreras { get; set; } = "carreras";
        public static string Ejemplares { get; set; } = "ejemplares";

        public static string Gemini { get; set; } = "gemini";

        public static string GetEndpoint(string name)
        {
            return name switch
            {
                nameof(Libros) => Libros,
                nameof(Autores) => Autores,
                nameof(Editoriales) => Editoriales,
                nameof(Generos) => Generos,
                nameof(LibroAutores) => LibroAutores,
                nameof(LibroGeneros) => LibroGeneros,
                nameof(UsuarioCarreras) => UsuarioCarreras,
                nameof(Usuarios) => Usuarios,
                nameof(Prestamos) => Prestamos,
                nameof(Carreras) => Carreras,
                nameof(Ejemplares) => Ejemplares,
                nameof(Gemini) => Gemini,
                _ => throw new ArgumentException($"Endpoint '{name}' no está definido.")
            };
        }
    }
}
