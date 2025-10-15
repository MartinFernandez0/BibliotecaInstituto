using Service.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Column("Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("Password")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Column("TipoRol")]
        public TipoRolEnum TipoRol { get; set; } = TipoRolEnum.Alumno;

        [Column("FechaRegistracion")]
        public DateTime FechaRegistracion { get; set; } = DateTime.Now;

        [Required]
        [Column("Dni")]
        public string Dni { get; set; } = string.Empty;

        [Column("Domicilio")]
        public string Domicilio { get; set; } = string.Empty;

        [Column("Telefono")]
        public string Telefono { get; set; } = string.Empty;

        [Column("Observacion")]
        public string Observacion { get; set; } = string.Empty;

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
