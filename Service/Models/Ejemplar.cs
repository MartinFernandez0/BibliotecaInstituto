using Service.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class Ejemplar
    {
        public int Id { get; set; }
        public int LibroId { get; set; }

        public Libro? Libro { get; set; }
        public bool Disponible { get; set; } = true;

        [Required]
        public EstadoEnum Estado { get; set; } = EstadoEnum.Exelente;
        public bool IsDeleted { get; set; } = false;
    }
}
