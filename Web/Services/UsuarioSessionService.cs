using Service.Models;
using Web.Interface;

namespace Web.Services
{
    public class UsuarioSessionService : IUsuarioSessionService
    {
        public Usuario? UsuarioLogueado { get; set; }
    }
}
