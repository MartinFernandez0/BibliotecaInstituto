using Service.Interfaces;

namespace Service.Models.InstitutoApp
{
    public class Docente : IEntityIdNombre, IEntityWithId
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Eliminado { get; set; } = false;


        public override string ToString()
        {
            return Nombre;
        }
    }
}
