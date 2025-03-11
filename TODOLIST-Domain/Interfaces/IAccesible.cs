
using Models.Entidades;
using TODO.Models.Dtos;

namespace TODO.Interfaces
{
    public interface IAccesible
    {
        Task<string> Login(LoginDTO modelo);
        Task<Usuario> Registro(RegistroDTO modelo);
        Task<List<Usuario>> GetUsuario();
    }
}
